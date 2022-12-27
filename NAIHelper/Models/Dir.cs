using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NAIHelper.Utils;

namespace NAIHelper.Models;

public class Dir : IdEntity
{
    public string  Name { get; set; } = string.Empty;
    public string? Link { get; set; }
    public string? Note { get; set; }

    public                                  int? IdParent  { get; set; }
    [ForeignKey("IdParent")] public virtual Dir? ParentDir { get; set; }

    public virtual ICollection<Dir> ChildDirs { get; set; }
    public virtual ICollection<Tag> Tags      { get; set; }
    
}