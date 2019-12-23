using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicExpression.Console
{
    public class UserSearchDto
    {
        public string UserId { set; get; }
        public DateTime? RegisterStartDate { set; get; }
        public DateTime? RegisterEndDate { set; get; }
        public string PhoneNumber { set; get; }
        public string Nick { set; get; }
    }
}
