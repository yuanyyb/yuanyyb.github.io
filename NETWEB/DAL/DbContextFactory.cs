﻿using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbContextFactory
    {
        /// <summary>
        /// 保证EF上下文实例线程内唯一.
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("db");
            if (dbContext == null)
            {
                dbContext = new DBEntities();
                CallContext.SetData("db", dbContext);
            }
            return dbContext;
        }
    }
}
