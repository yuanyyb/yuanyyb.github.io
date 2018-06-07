using DAL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DALFactory
{
    public partial class DBSession:IDBSession
    {
        //OAEntities Db = new OAEntities();
        public DbContext Db
        {
            get { return DbContextFactory.CreateDbContext(); }
        }
        private IUserInfoDAL _UserInfoDal;
        public IUserInfoDAL UserInfoDAL
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    //_UserInfoDal = new UserInfoDal();//变化点，通过抽象工厂。
                    _UserInfoDal = AbstractFactory.CreateUserInfoDAL();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }

        /// <summary>
        /// 一个业务中某个方法要涉及到对多张表的操作，那么我们可以将操作的数据加载到EF上下文中，最后调用该方法（SaveChanges），将数据一次性保持到数据库中，这样完成连接一次数据库多操作。提高数据库的性能。工作单元模式(UnitOfWork)
        /// </summary>
        /// <returns></returns>

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">insert, delete,update</param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params SqlParameter[] pars)
        {
            return Db.Database.ExecuteSqlCommand(sql, pars);
        }
        /// <summary>
        /// 执行select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public List<T> ExecuteQuerySql<T>(string sql, params SqlParameter[] pars)
        {
            return Db.Database.SqlQuery<T>(sql, pars).ToList();
        }
    }
}
