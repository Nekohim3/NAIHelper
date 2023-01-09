using NAIHelper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIHelper.ViewModels.UI_Entities;

[JsonObject]
public abstract class ManyToManyEntity<T1, T2> : TrackedEntity where T1 : IdEntity where T2 : IdEntity
{
    public int IdFirst  { get; set; }
    public int IdSecond { get; set; }
    public T1? First    { get; set; }
    public T2? Second   { get; set; }

    protected ManyToManyEntity()
    {
        
    }

    protected ManyToManyEntity(int idFirst, int idSecond)
    {
        IdFirst  = idFirst;
        IdSecond = idSecond;
    }

    protected ManyToManyEntity(T1 first, T2 second)
    {
        First    = first;
        Second   = second;
        IdFirst  = first.Id;
        IdSecond = second.Id;
    }
}
