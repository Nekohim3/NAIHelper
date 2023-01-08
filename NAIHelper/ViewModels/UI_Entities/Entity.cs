using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils;
using Newtonsoft.Json;

namespace NAIHelper.ViewModels.UI_Entities;

[JsonObject]
public class Entity : ViewModelBase
{
    public              int     Id       { get; set; }
    protected           byte[]? InitHash { get; set; }
    [JsonIgnore] public string  Md5      => InitHash == null ? string.Empty : InitHash.Aggregate("", (current, x) => current + x.ToString("X2"));

    [JsonIgnore] public bool IsChanged => InitHash == null || !this.GetHash().SequenceEqual(InitHash);

    public void CreateHash()
    {
        InitHash = this.GetHash();
    }

    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null)
            return true;
        if (a is null || b is null)
            return false;
        return a.Equals(b);
    }

    public override bool Equals(object? o)
    {
        if (o is not Entity e)
            return false;
        if (e.Id                   == 0 && Id == 0)
            return e.GetHashCode() == GetHashCode();
        return e.Id == Id;
    }
}
