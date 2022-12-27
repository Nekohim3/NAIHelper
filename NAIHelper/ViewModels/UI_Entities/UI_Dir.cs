using System.Linq;
using NAIHelper.Models;
using NAIHelper.Utils;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

public class UI_Dir : UI_Entity
{
    #region Entity properties

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string? _link;
    public string? Link
    {
        get => _link;
        set => this.RaiseAndSetIfChanged(ref _link, value);
    }

    private string? _note;
    public string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    private UI_Dir? _uI_ParentDir;
    public UI_Dir? UI_ParentDir
    {
        get => _uI_ParentDir;
        set => this.RaiseAndSetIfChanged(ref _uI_ParentDir, value);
    }

    private ObservableCollectionWithSelectedItem<UI_Dir> _uI_Dirs;
    public ObservableCollectionWithSelectedItem<UI_Dir> UI_Dirs
    {
        get => _uI_Dirs;
        set => this.RaiseAndSetIfChanged(ref _uI_Dirs, value);
    }

    private ObservableCollectionWithSelectedItem<UI_Tag> _uI_Tags;
    public ObservableCollectionWithSelectedItem<UI_Tag> UI_Tags
    {
        get => _uI_Tags;
        set => this.RaiseAndSetIfChanged(ref _uI_Tags, value);
    }

    #endregion

    #region Properties

    #endregion

    #region Ctor

    public UI_Dir()
    {
        
    }

    public UI_Dir(string name, string? link = null, string? note = null)
    {
        _name = name;
        _link = link;
        _note = note;
    }

    #endregion
    
}