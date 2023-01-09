using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils;
using Newtonsoft.Json;

namespace NAIHelper.ViewModels.UI_Entities;

[JsonObject]
public abstract class IdEntity : TrackedEntity
{
    public              int     Id       { get; set; }
    
    public override bool Equals(object? o)
    {
        if (o is not IdEntity e)
            return false;
        if (e.Id                   == 0 && Id == 0)
            return e.GetHashCode() == GetHashCode();
        return e.Id == Id;
    }
}
