using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NAIHelper.Utils;

namespace NAIHelper.Models;

public class Tag : IdEntity
{
    public string  Name { get; set; } = string.Empty;
    public string? Link { get; set; }
    public string? Note { get; set; }

    public                               int IdDir { get; set; }
    [ForeignKey("IdDir")] public virtual Dir Dir   { get; set; }
}