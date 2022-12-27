using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils;

namespace NAIHelper.ViewModels.UI_Entities
{
    public class UI_Entity : ViewModelBase
    {
        public int Id       { get; set; }
        protected  int InitHash { get; set; }
        public static bool operator !=(UI_Entity? a, UI_Entity? b)
        {
            return !(a == b);
        }

        public static bool operator ==(UI_Entity? a, UI_Entity? b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        public override bool Equals(object? o)
        {
            if (o is not UI_Entity e)
                return false;
            if (e.Id                   == 0 && Id == 0)
                return e.GetHashCode() == GetHashCode();
            return e.Id == Id;
        }

        public override int GetHashCode() => this.GetHash();

    }
}
