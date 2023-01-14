using NAIHelper.Database.UI_Entities.BaseEntities;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.Database.UI_Entities;

public class GroupTag : IdEntity
{
    #region Entity properties

    private int _order;
    public int Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }

    private int _strength;
    public int Strength
    {
        get => _strength;
        set => this.RaiseAndSetIfChanged(ref _strength, value);
    }

    private int _idGroup;
    public int IdGroup
    {
        get => _idGroup;
        set => this.RaiseAndSetIfChanged(ref _idGroup, value);
    }

    private Group _group;
    [JsonIgnore]
    public Group Group
    {
        get => _group;
        set => this.RaiseAndSetIfChanged(ref _group, value);
    }

    private int _idTag;
    public int IdTag
    {
        get => _idTag;
        set => this.RaiseAndSetIfChanged(ref _idTag, value);
    }

    private Tag _tag;
    [JsonIgnore]
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