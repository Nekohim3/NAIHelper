using System.Linq;
using NAIHelper.Utils;
using NAIHelper.ViewModels.UI_Entities.BaseEntities;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

public class Session : IdEntity
{
    #region Entity properties

    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    private string? _note;
    public string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    private ObservableCollectionWithSelectedItem<Group> _groups;
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