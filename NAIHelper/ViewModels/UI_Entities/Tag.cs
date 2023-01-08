using System;
using NAIHelper.Utils.Interfaces;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

[JsonObject]
public class Tag : Entity, ISelected, IDraggable
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

    private int _idDir;
    public int IdDir
    {
        get => _idDir;
        set => this.RaiseAndSetIfChanged(ref _idDir, value);
    }

    private Dir? _dir;
    [JsonIgnore]
    public Dir? Dir
    {
        get => _dir;
        set => this.RaiseAndSetIfChanged(ref _dir, value);
    }

    #endregion

    #region NestedProperties

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }

    private bool _isDrag;
    public bool IsDrag
    {
        get => _isDrag;
        set => this.RaiseAndSetIfChanged(ref _isDrag, value);
    }

    #endregion

    #region Properties

    #endregion

    #region Ctor

    public Tag()
    {
    }

    public Tag(string name, string? link = null, string? note = null)
    {
        _name = name;
        _link = link;
        _note = note;
    }

    #endregion
}
