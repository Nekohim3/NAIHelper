using NAIHelper.Models;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

public class UI_Tag : UI_Entity
{
    #region Entity properties

    private string _name;
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
    

    private UI_Dir _uI_Dir;
    public UI_Dir UI_Dir
    {
        get => _uI_Dir;
        set => this.RaiseAndSetIfChanged(ref _uI_Dir, value);
    }

    #endregion

    #region Properties



    #endregion


    #region Ctor

    public UI_Tag()
    {
        
    }

    public UI_Tag(string name, string? link = null, string? note = null)
    {
        
    }

    #endregion
}