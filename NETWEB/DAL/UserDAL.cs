using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UserDAL
    {
        public List<UserInfo> GetAll(string constring)
        {
            IDbConnection conn = new SqlConnection(constring);
            List<UserInfo> userlist = new List<UserInfo>();
            UserInfo user = new UserInfo();
            string sql = "select * from UserInfo";
            userlist = Dapper.SqlMapper.Query<UserInfo>(conn, sql).ToList<UserInfo>();
            return userlist;

        }
    }
}
