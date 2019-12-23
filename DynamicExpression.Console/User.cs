using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicExpression.Console
{
    public class User
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 生成的guid，用来混合加密用户密码
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 升级时间
        /// </summary>
        public DateTime? UpgradeTime { get; set; }
    }
}
