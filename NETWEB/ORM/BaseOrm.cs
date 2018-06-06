﻿using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class BaseOrm
    {
        //string constring = "Data Source=bds274600440.my3w.com;Initial Catalog=bds274600440_db;User ID=bds274600440;Password=yyb18yyb;";
        string constring = "data source=bds274600440.my3w.com;initial catalog=bds274600440_db;persist security info=True;user id=bds274600440;password=yyb18yyb;";

        

        
        public List<UserInfo> GetAll() {
            IDbConnection conn = new SqlConnection(constring);
            List<UserInfo> userlist = new List<UserInfo>();
            UserInfo user = new UserInfo();
            string sql = "select * from UserInfo";
            userlist = Dapper.SqlMapper.Query<UserInfo>(conn,sql).ToList<UserInfo>();
            return userlist;

        }
    }
}
