using System.Linq;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.Database.UI_Entities;

public class Group : IdEntity
{
    #region Entity properties

    private string _name = string.Empty;
    [TrackInclude]
    [JsonProperty]
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private int _order;
    [TrackInclude]
    [JsonProperty]
    public int Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }

    private string? _note;
    [TrackInclude]
    [JsonProperty]
    public string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    private int _idSession;
    [TrackInclude]
    [JsonProperty]
    public int IdSession
    {
        get => _idSession;
        set => this.RaiseAndSetIfChanged(ref _idSession, value);
    }

    private Session _session;
    public Session Session
    {
        get => _session;
        set => this.RaiseAndSetIfChanged(ref _session, value);
    }

    private ObservableCollectionWithSelectedItem<GroupTag> _groupTags;
    [JsonProperty]
    public ObservableCollectionWithSelectedItem<GroupTag> GroupTags
    {
        get => _groupTags;
        set => this.RaiseAndSetIfChanged(ref _groupTags, value);
    }

    #endregion

    #region Properties



    #endregion

    #region Ctor

    public Group()
    {

    }

    public Group(string name, int order = 0, string? note = null)
    {
        _name = name;
        _order = order;
        _note = note;
    }

    #endregion
}