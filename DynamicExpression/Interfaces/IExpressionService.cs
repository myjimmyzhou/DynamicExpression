using DynamicExpression.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DynamicExpression.Interfaces
{
    public interface IExpressionService<T>
    {
        Expression<Func<T, bool>> GenerateExpression(List<PropertyModel> list);
        Expression<Func<T, bool>> GenerateExpression(List<PropertyModel<T>> list);
    }
}
