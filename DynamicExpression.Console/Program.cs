using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicExpression.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // 模拟数据源
            List<User> list = new List<User>
            {
                new User
                {
                    UserId = "123456",
                    Nick = "nick",
                    PhoneNumber = "18077774444",
                    RegisterTime = DateTime.Now,
                    CreateTime = DateTime.Now
                },
                new User
                {
                    UserId = "123457",
                    Nick = "nick",
                    PhoneNumber = "18077774444",
                    RegisterTime = DateTime.Now,
                    CreateTime = DateTime.Now
                },
                new User
                {
                    UserId = "123458",
                    Nick = "nick",
                    PhoneNumber = "18077774444",
                    RegisterTime = DateTime.Now,
                    CreateTime = DateTime.Now
                }
            };
            IQueryable<User> users = Queryable.AsQueryable(list);
            // 条件筛选用户。前端传回参数模型UserSearchDto,部分字段可能为空
            UserSearchDto userSearch = new UserSearchDto
            {
                UserId = "123456"
            };
            // 组合查询表达式树
            Expression<Func<User, bool>> expression = u
             => u.RegisterTime >= userSearch.RegisterStartDate
             && u.UserId == userSearch.UserId
             && u.RegisterTime <= userSearch.RegisterEndDate
             && u.PhoneNumber.Contains(userSearch.PhoneNumber)
             && u.Nick.Contains(userSearch.Nick);
            // 过滤null 条件,执行查询
            Expression<Func<User, bool>> condition = expression.FilterNull();
            var result = users.Where(condition).ToList();

            foreach(var u in result)
            {
                System.Console.WriteLine(u.UserId);
            }
            System.Console.ReadKey();
        }
    }
}
