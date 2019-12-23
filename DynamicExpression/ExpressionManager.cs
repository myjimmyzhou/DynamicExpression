using DynamicExpression.Models;
using DynamicExpression.Processors;
using DynamicExpression.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace DynamicExpression
{
    public class ExpressionManager
    {
        public static Expression<Func<T, bool>> GetExpression<T>(List<PropertyModel> list)
        {
            return new ExpressionService<T>().GenerateExpression(list);
        }

        public static Expression<Func<T, bool>> GetExpression<T>(List<PropertyModel<T>> list)
        {
            return new ExpressionService<T>().GenerateExpression(list);
        }

        public static Expression<Func<T, bool>> ExpressionConvert<T>(Expression<Func<T, bool>> expression)
        {
            var parameters = expression.Parameters;
            List<ExpressionModel> modelList = new List<ExpressionModel>();
            SplitExpressions<T>(expression.Body, parameters, modelList);
            if (modelList.Count > 0) modelList.Reverse();
            var r = CombineExpressionHandler<T>(modelList);
            if (r == null) r = e => true;
            return r;
        }

        private static Expression<Func<T, bool>> CombineExpressionHandler<T>(List<ExpressionModel> list)
        {
            var p = new ExpressionProcessor<T>();
            Expression<Func<T, bool>> exp = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    exp = (Expression<Func<T, bool>>)list[i].LambdaExpression;
                }
                else
                {
                    exp = p.CombineExpression(exp, (Expression<Func<T, bool>>)list[i].LambdaExpression, list[i].ExpressionType);
                }
            }
            return exp;
        }

        private static void SplitExpressions<T>(Expression expression, ReadOnlyCollection<ParameterExpression> parameters, List<ExpressionModel> list)
        {
            if (expression is BinaryExpression binaryBody)
            {
                Expression right = binaryBody.Right;
                var v = GetVaule<T>(right);
                if (v != null)
                {
                    var lambaExpress = Expression.Lambda(right, parameters);
                    list.Add(new ExpressionModel { LambdaExpression = lambaExpress, ExpressionType = binaryBody.NodeType });
                }
                else
                {

                }
                var left = binaryBody.Left;
                if (left.ToString().IndexOf("AndAlso") != -1 || left.ToString().IndexOf("OrElse") != -1)
                {
                    SplitExpressions<T>((BinaryExpression)left, parameters, list);
                }
                else
                {
                    var v1 = GetVaule<T>(left);
                    if (v1 != null)
                    {
                        var _lambaExpress = Expression.Lambda(left, parameters);
                        list.Add(new ExpressionModel { LambdaExpression = _lambaExpress, ExpressionType = binaryBody.NodeType });
                    }
                }
            }
        }

        internal static object GetVaule<T>(Expression expression)
        {
            object result = null;
            if (expression is BinaryExpression)
            {
                result = GetMemberExpressionValue(((BinaryExpression)expression).Right);
                //result = GetVaule<T>(((BinaryExpression)expression).Left, obj);
            }
            else if (expression is MethodCallExpression callExp)
            {
                switch (callExp.Arguments[0].NodeType)
                {
                    case ExpressionType.MemberAccess:
                        var _exprssion = GetMemberExpressionValue((MemberExpression)callExp.Arguments[0]);
                        if (null != _exprssion)
                        {
                            result = _exprssion.ToString();
                        }
                        break;
                    case ExpressionType.Constant:
                        result = ((ConstantExpression)callExp.Arguments[0]).Value.ToString();
                        break;
                }
            }
            else if (expression is MemberExpression)
            {
                result = GetMemberExpressionValue((MemberExpression)expression);
            }
            else if (expression is ConstantExpression)
            {
                return ((ConstantExpression)expression).Value;
            }
            else
            {
                return 0;
            }
            return result;
        }

        private static object GetMemberExpressionValue(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }
    }
}
