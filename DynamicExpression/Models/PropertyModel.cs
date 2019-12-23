using DynamicExpression.Enums;
using System;
using System.Linq.Expressions;

namespace DynamicExpression.Models
{
    public class PropertyModel
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        private ExpressionType? _expresionType;
        public ExpressionType ExpressionType
        {
            get
            {
                if (!_expresionType.HasValue) return ExpressionType.AndAlso;
                else return _expresionType.Value;
            }
            set
            {
                _expresionType = value;
            }
        }
        public OperationType OperationType { get; set; }
    }

    public class PropertyModel<T>
    {
        public Expression<Func<T, object>> PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public ExpressionType ExpressionType { get; set; }
        public OperationType OperationType { get; set; }
    }
}
