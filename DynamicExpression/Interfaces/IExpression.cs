using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DynamicExpression.Interfaces
{
    public interface IExpression<T>
    {
        /// <summary>
        /// 合并2个Expression;目前只接受四种情况：And、AndAlso、Or、OrElse。其它的返回null
        /// </summary>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <param name="expressionType"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> CombineExpression(Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2, ExpressionType expressionType);
        /// <summary>
        /// 合并Expression
        /// </summary>
        /// <param name="expressions"></param>
        /// <param name="expressionType"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> CombineExpression(List<Expression<Func<T, bool>>> expressions, ExpressionType expressionType = ExpressionType.AndAlso);
        /// <summary>
        /// lambda表达式：p => true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Expression<Func<T, bool>> True();
        /// <summary>
        /// lambda表达式：p => p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> Equal(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => p.propertyName != propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> NotEqual(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => p.propertyName > propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> GreaterThan(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => p.propertyName < propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> LessThan(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => p.propertyName >= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> GreaterThanOrEqual(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => p.propertyName <= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> LessThanOrEqual(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> Contains(string propertyName, object propertyValue);
        /// <summary>
        /// lambda表达式：p => !p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        //Expression<Func<T, bool>> NotContains(string propertyName, object propertyValue);
        //Expression<Func<T, bool>> Like(string propertyName, object propertyValue);
    }
}
