
using DALFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL
{
    public abstract class BaseBLL<T> where T:class,new ()
    {
       public IDAL.IDBSession GetDbSession
       {
           get { return DBSessionFactory.CreateDbSession(); }
       }
       public IDAL.IBaseDAL<T> CurrentDal { get; set; }
       public abstract void SetCurrentDal();
       public BaseBLL()
       {
          SetCurrentDal();//子类必须要实现该抽象方法
       }


        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDal.LoadEntities(whereLambda);
        }
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return this.CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            this.CurrentDal.DeleteEntity(entity);
            return this.GetDbSession.SaveChanges();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateEntity(T entity)
        {
            this.CurrentDal.UpdateEntity(entity);
            return this.GetDbSession.SaveChanges();
        }
        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            this.CurrentDal.AddEntity(entity);
            this.GetDbSession.SaveChanges();
            return entity;
        }
    }
}
