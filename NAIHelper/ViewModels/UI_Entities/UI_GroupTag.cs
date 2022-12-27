using NAIHelper.Models;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

public class UI_GroupTag : UI_Entity
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
    

    private UI_Group _uI_Group;
    public UI_Group UI_Group
    {
        get => _uI_Group;
        set => this.RaiseAndSetIfChanged(ref _uI_Group, value);
    }

    private UI_Tag _uI_Tag;
    public UI_Tag UI_Tag
    {
        get => _uI_Tag;
        set => this.RaiseAndSetIfChanged(ref _uI_Tag, value);
    }

    #endregion

    #region Properties



    #endregion


    #region Ctor

    public UI_GroupTag()
    {
        
    }

    public UI_GroupTag(int order, int strength)
    {
        
    }

    #endregion
}