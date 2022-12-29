using System.Collections.Generic;
using Newtonsoft.Json;

namespace NAIHelper.Models;

public class Session : IdEntity
{
    public string  Name { get; set; } = string.Empty;
    public string? Note { get; set; }

    [JsonIgnore] public virtual ICollection<Group> Groups { get; set; }
}
