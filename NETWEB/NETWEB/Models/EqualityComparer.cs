using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETWEB.Models
{
    public class EqualityComparer:IEqualityComparer<ActionInfo>
    {
        public bool Equals(ActionInfo x, ActionInfo y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(ActionInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}