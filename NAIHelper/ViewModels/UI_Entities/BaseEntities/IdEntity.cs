using NAIHelper.Utils;
using Newtonsoft.Json;

namespace NAIHelper.ViewModels.UI_Entities.BaseEntities;

public abstract class IdEntity : Entity // : TrackedEntity
{
    [TrackInclude]
    [JsonProperty]
    public virtual int Id { get; set; }

    public override bool Equals(object? o)
    {
        if (o is not IdEntity e)
            return false;
        if (e.Id                   == 0 && Id == 0)
            return e.GetHashCode() == GetHashCode();
        return e.Id == Id;
    }
}
