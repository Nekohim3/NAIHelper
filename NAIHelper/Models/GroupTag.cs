using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NAIHelper.Models;

public class GroupTag : IdEntity
{
    public int Order    { get; set; }
    public int Strength { get; set; }

    public                                             int   IdGroup { get; set; }
    [JsonIgnore] [ForeignKey("IdPart")] public virtual Group Group   { get; set; }

    public                                            int IdTag { get; set; }
    [JsonIgnore] [ForeignKey("IdTag")] public virtual Tag Tag   { get; set; }
}
