using System.Linq;
using NAIHelper.Models;
using NAIHelper.Utils;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

public class UI_Group : UI_Entity
{
    #region Entity properties

    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private int _order;
    public int Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }

    private string? _note;
    public string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    private UI_Session _uI_Session;
    public UI_Session UI_Session
    {
        get => _uI_Session;
        set => this.RaiseAndSetIfChanged(ref _uI_Session, value);
    }

    private ObservableCollectionWithSelectedItem<UI_GroupTag> _uI_GroupTags;
    public ObservableCollectionWithSelectedItem<UI_GroupTag> UI_GroupTags
    {
        get => _uI_GroupTags;
        set => this.RaiseAndSetIfChanged(ref _uI_GroupTags, value);
    }

    #endregion

    #region Properties



    #endregion


    #region Ctor

    public UI_Group()
    {
        
    }

    public UI_Group(string name, int order = 0, string? note = null)
    {
        _name = name;
        _order = order;
        _note = note;
    }

    #endregion
}