using System;
using System.Linq;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils;
using NAIHelper.Utils.Interfaces;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.Database.UI_Entities;

public class Tag : IdEntity, ISelected, IDraggable
{
    #region Entity properties

    private string _name = string.Empty;
    [TrackInclude]
    [JsonProperty]
    public virtual string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string? _link;
    [TrackInclude]
    [JsonProperty]
    public virtual string? Link
    {
        get => _link;
        set => this.RaiseAndSetIfChanged(ref _link, value);
    }

    private string? _note;
    [TrackInclude]
    [JsonProperty]
    public virtual string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    //private int _idDir;
    //public int IdDir
    //{
    //    get => _idDir;
    //    set => this.RaiseAndSetIfChanged(ref _idDir, value);
    //}

    private ObservableCollectionWithSelectedItem<Dir> _dirs = new();
    public ObservableCollectionWithSelectedItem<Dir> Dirs
    {
        get => _dirs;
        set => this.RaiseAndSetIfChanged(ref _dirs, value);
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

    public string Paths => $"{Name}\n{string.Join('\n', Dirs.Select(_ => _.Path))}";

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
