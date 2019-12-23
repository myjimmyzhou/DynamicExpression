using DynamicExpression.Enums;
using DynamicExpression.Interfaces;
using DynamicExpression.Models;
using DynamicExpression.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicExpression.Services
{
    public class ExpressionService<T> : IExpressionService<T>
    {
        private IExpression<T> expression = new ExpressionProcessor<T>();

        public Expression<Func<T, bool>> GenerateExpression(List<PropertyModel> list)
        {
            Expression<Func<T, bool>> func1 = null;
            if (list.Count == 1)
            {
                var item = list.First();
                if (item.PropertyValue == null)
                {
                    return expression.True();
                }
                func1 = GetExpression(item);
            }
            else
            {
                foreach (var item in list)
                {
                    if (item.PropertyValue != null)
                    {
                        Expression<Func<T, bool>> func2 = GetExpression(item);
                        if (func1 != null)
                        {
                            func1 = expression.CombineExpression(func1, func2, item.ExpressionType);
                        }
                        else
                        {
                            func1 = func2;
                        }
                    }
                }
                if (func1 == null)
                {
                    return expression.True();
                }
            }
            return func1;
        }

        public Expression<Func<T, bool>> GenerateExpression(List<PropertyModel<T>> list)
        {
            List<PropertyModel> propertyModels = new List<PropertyModel>();
            list.ForEach(x =>
            {
                propertyModels.Add(new PropertyModel
                {
                    OperationType = x.OperationType,
                    ExpressionType = x.ExpressionType,
                    PropertyName = GetFuncName(x.PropertyName),
                    PropertyValue = x.PropertyValue
                });
            });
            return GenerateExpression(propertyModels);
        }

        protected Expression<Func<T, bool>> GetExpression(PropertyModel item)
        {
            Expression<Func<T, bool>> func = null;
            switch (item.OperationType)
            {
                case OperationType.Equal:
                    func = expression.Equal(item.PropertyName, item.PropertyValue);
                    break;
                case OperationType.NotEqual:
                    func = expression.NotEqual(item.PropertyName, item.PropertyValue);
                    break;
                case OperationType.GreaterThan:
                    func = expression.GreaterThan(item.PropertyName, item.PropertyValue);
                    break;
                case OperationType.GreaterThanOrEqual:
                    func = expression.GreaterThanOrEqual(item.PropertyName, item.PropertyValue);
                    break;
                case OperationType.LessThan:
                    func = expression.LessThan(item.PropertyName, item.PropertyValue);
                    break;
                case OperationType.LessThanOrEqual:
                    func = expression.LessThanOrEqual(item.PropertyName, item.PropertyValue);
                    break;
                case OperationType.Contains:
                    func = expression.Contains(item.PropertyName, item.PropertyValue);
                    break;
                default:
                    func = expression.True();
                    break;
            }
            return func;
        }

        internal string GetFuncName(Expression<Func<T, object>> propertyName)
        {
            string name = null;
            if (propertyName.Body is UnaryExpression)
            {
                name = ((MemberExpression)((UnaryExpression)propertyName.Body).Operand).Member.Name;
            }
            else if (propertyName.Body is MemberExpression)
            {
                name = ((MemberExpression)propertyName.Body).Member.Name;
            }
            else if (propertyName.Body is ParameterExpression)
            {
                name = ((ParameterExpression)propertyName.Body).Type.Name;
            }
            return name;
        }
    }
}
