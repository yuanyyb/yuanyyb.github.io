using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
   public enum DelFlag
    {
       /// <summary>
       /// 正常
       /// </summary>
       Normarl=0,
       /// <summary>
       /// 逻辑删除
       /// </summary>
       LogicDelete=1,
       /// <summary>
       /// 物理删除
       /// </summary>
       PhysicDelete=2

    }
}
