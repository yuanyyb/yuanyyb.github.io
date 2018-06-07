using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class UserInfoBLL:BaseBLL<UserInfo>,IUserInfoBLL
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.GetDbSession.UserInfoDAL;
        }

    }
}
