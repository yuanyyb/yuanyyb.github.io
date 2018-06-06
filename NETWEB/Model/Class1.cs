using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Class1
    {
        public List<UserInfo> getUser()
        {
            DBEntities db = new DBEntities();
            List<UserInfo> list = (from u in db.UserInfo select u).ToList<UserInfo>();
            return list;
        }
    }
}
