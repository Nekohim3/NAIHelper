using System.Linq;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.Database.UI_Entities;

public class Session : IdEntity
{
    #region Entity properties

    private string _name;
    [TrackInclude]
    [JsonProperty]
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    private string? _note;
    [TrackInclude]
    [JsonProperty]
    public string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    private ObservableCollectionWithSelectedItem<Group> _groups;
    [TrackInclude]
    [JsonProperty]
    public ObservableCollectionWithSelectedItem<Group> Groups
    {
        get => _groups;
        set => this.RaiseAndSetIfChanged(ref _groups, value);
    }

    #endregion

    #region Properties



    #endregion


    #region Ctor

    public Session()
    {

    }

    public Session(string name, string? note = null)
    {
        _name = name;
        _note = note;
    }

    #endregion
}