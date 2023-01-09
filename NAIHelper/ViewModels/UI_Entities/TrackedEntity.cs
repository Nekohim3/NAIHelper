using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils;
using Newtonsoft.Json;

namespace NAIHelper.ViewModels.UI_Entities
{
    [JsonObject]
    public abstract class TrackedEntity : ViewModelBase
    {
        protected           byte[]? InitHash { get; set; }
        [JsonIgnore] public string  Md5      => InitHash == null ? string.Empty : InitHash.Aggregate("", (current, x) => current + x.ToString("X2"));

        [JsonIgnore] public bool IsChanged => InitHash == null || !this.GetHash().SequenceEqual(InitHash);
        public void CreateHash()
        {
            InitHash = this.GetHash();
        }

        public static bool operator !=(TrackedEntity? a, TrackedEntity? b)
        {
            return !(a == b);
        }

        public static bool operator ==(TrackedEntity? a, TrackedEntity? b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }
    }
}
