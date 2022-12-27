using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NAIHelper.Models;

public class Group : IdEntity
{
    public string  Name  { get; set; }
    public int     Order { get; set; }
    public string? Note  { get; set; }

    public                                   int     IdSession { get; set; }
    [ForeignKey("IdSession")] public virtual Session Session   { get; set; }

    public virtual ICollection<GroupTag> GroupTags { get; set; }
    
}