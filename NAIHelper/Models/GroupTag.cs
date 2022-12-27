using System.ComponentModel.DataAnnotations.Schema;

namespace NAIHelper.Models;

public class GroupTag : IdEntity
{
    public int Order    { get; set; }
    public int Strength { get; set; }

    public                                int   IdGroup { get; set; }
    [ForeignKey("IdPart")] public virtual Group Group   { get; set; }

    public                               int IdTag { get; set; }
    [ForeignKey("IdTag")] public virtual Tag Tag   { get; set; }
    
}