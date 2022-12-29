using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NAIHelper.Models;

public class Group : IdEntity
{
    public string  Name  { get; set; }
    public int     Order { get; set; }
    public string? Note  { get; set; }

    public                                                int     IdSession { get; set; }
    [JsonIgnore] [ForeignKey("IdSession")] public virtual Session Session   { get; set; }

    [JsonIgnore] public virtual ICollection<GroupTag> GroupTags { get; set; }
}
