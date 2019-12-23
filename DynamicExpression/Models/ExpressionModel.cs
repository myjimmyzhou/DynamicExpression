using DynamicExpression.Enums;
using System.Linq.Expressions;

namespace DynamicExpression.Models
{
    public class ExpressionModel
    {
        public LambdaExpression LambdaExpression { get; set; }
        public ExpressionType ExpressionType { get; set; }
    }
}
