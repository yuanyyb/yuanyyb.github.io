using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IDBSession
    {
        IUserInfoDAL UserInfoDAL { get; set; }
        bool SaveChanges();
        int ExecuteSql(string sql, params SqlParameter[] pars);
        List<T> ExecuteQuerySql<T>(string sql, params SqlParameter[] pars);
    }
}
