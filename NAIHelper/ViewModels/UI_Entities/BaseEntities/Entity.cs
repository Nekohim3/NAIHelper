using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIHelper.ViewModels.UI_Entities.BaseEntities
{
    public class Entity : TrackedEntity
    {
        public override bool Equals(object? o)
        {
            if (o is not IdEntity e)
                return false;
            return e.GetHashCode() == GetHashCode();
        }
    }
}
