using System.Linq;
using NAIHelper.Models;
using NAIHelper.Utils;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

public class UI_Session : UI_Entity
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

    private ObservableCollectionWithSelectedItem<UI_Group> _uI_Groups;
    public ObservableCollectionWithSelectedItem<UI_Group> UI_Groups
    {
        get => _uI_Groups;
        set => this.RaiseAndSetIfChanged(ref _uI_Groups, value);
    }

    #endregion

    #region Properties



    #endregion


    #region Ctor

    public UI_Session()
    {
        
    }

    public UI_Session(string name, string? note = null)
    {
        
    }

    #endregion
}