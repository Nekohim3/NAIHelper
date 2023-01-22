using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.Database.UI_Entities;

public class GroupTag : IdEntity
{
    #region Entity properties

    private int _order;
    [TrackInclude]
    [JsonProperty]
    public int Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }

    private int _strength;
    [TrackInclude]
    [JsonProperty]
    public int Strength
    {
        get => _strength;
        set => this.RaiseAndSetIfChanged(ref _strength, value);
    }

    private int _idGroup;
    [TrackInclude]
    [JsonProperty]
    public int IdGroup
    {
        get => _idGroup;
        set => this.RaiseAndSetIfChanged(ref _idGroup, value);
    }

    private Group _group;
    public Group Group
    {
        get => _group;
        set => this.RaiseAndSetIfChanged(ref _group, value);
    }

    private int _idTag;
    [TrackInclude]
    [JsonProperty]
    public int IdTag
    {
        get => _idTag;
        set => this.RaiseAndSetIfChanged(ref _idTag, value);
    }

    private Tag _tag;
    public Tag Tag
    {
        get => _tag;
        set => this.RaiseAndSetIfChanged(ref _tag, value);
    }

    #endregion

    #region Properties



    #endregion


    #region Ctor

    public GroupTag()
    {

    }

    public GroupTag(int order, int strength)
    {
        _order = order;
        _strength = strength;
    }

    #endregion
}