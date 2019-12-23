using DynamicExpression.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicExpression.Processors
{
    public class ExpressionProcessor<T> : IExpression<T>
    {
        private static readonly string name = "p";
        protected static ParameterExpression parameter = Expression.Parameter(typeof(T), name);

        public Expression<Func<T, bool>> CombineExpression(Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2, ExpressionType expressionType)
        {
            var invokedExpr = Expression.Invoke(exp2, exp1.Parameters.Cast<Expression>());
            switch (expressionType)
            {
                case ExpressionType.AndAlso:
                    return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exp1.Body, invokedExpr), exp1.Parameters);
                case ExpressionType.OrElse:
                    return Expression.Lambda<Func<T, bool>>(Expression.OrElse(exp1.Body, invokedExpr), exp1.Parameters);
                case ExpressionType.And:
                    return Expression.Lambda<Func<T, bool>>(Expression.And(exp1.Body, invokedExpr), exp1.Parameters);
                case ExpressionType.Or:
                    return Expression.Lambda<Func<T, bool>>(Expression.Or(exp1.Body, invokedExpr), exp1.Parameters);
                default:
                    return null;
            }
        }

        public Expression<Func<T, bool>> CombineExpression(List<Expression<Func<T, bool>>> expressions, ExpressionType expressionType = ExpressionType.AndAlso)
        {
            Expression<Func<T, bool>> expression = expressions.FirstOrDefault();
            for (int i = 0; i < expressions.Count; i++)
            {
                if (i < expressions.Count - 1)
                {
                    expression = CombineExpression(expression, expressions[i + 1], expressionType);
                }
            }
            return expression;
        }

        public Expression<Func<T, bool>> Equal(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
        }

        public Expression<Func<T, bool>> NotEqual(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
        }

        public Expression<Func<T, bool>> GreaterThan(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
        }

        public Expression<Func<T, bool>> GreaterThanOrEqual(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            //ConstantExpression constant = Expression.Constant(propertyValue);
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        public Expression<Func<T, bool>> Contains(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
        }

        public Expression<Func<T, bool>> LessThan(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
        }

        public Expression<Func<T, bool>> LessThanOrEqual(string propertyName, object propertyValue)
        {
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Convert(Expression.Constant(propertyValue), member.Type);
            return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
        }

        public Expression<Func<T, bool>> True()
        {
            return p => true;
        }
    }
}
