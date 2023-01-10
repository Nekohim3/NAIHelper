using System;
using System.Collections.Generic;
using System.Linq;
using NAIHelper.Utils;
using NAIHelper.Utils.Interfaces;
using NAIHelper.ViewModels.UI_Entities.BaseEntities;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.ViewModels.UI_Entities;

[JsonObject]
public class Dir : IdEntity, ISelected, IExpanded
{
    #region Entity properties

    private string _name = string.Empty;
    [TrackInclude]
    public virtual string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string? _link;
    [TrackInclude]
    public virtual string? Link
    {
        get => _link;
        set => this.RaiseAndSetIfChanged(ref _link, value);
    }

    private string? _note;
    [TrackInclude]
    public virtual string? Note
    {
        get => _note;
        set => this.RaiseAndSetIfChanged(ref _note, value);
    }

    private int? _idParent;
    [TrackInclude]
    public virtual int? IdParent
    {
        get => _idParent;
        set => this.RaiseAndSetIfChanged(ref _idParent, value);
    }

    private Dir? _parentDir;
    public Dir? ParentDir
    {
        get => _parentDir;
        set => this.RaiseAndSetIfChanged(ref _parentDir, value);
    }

    private ObservableCollectionWithSelectedItem<Dir> _dirs = new();
    public ObservableCollectionWithSelectedItem<Dir> Dirs
    {
        get => _dirs;
        set => this.RaiseAndSetIfChanged(ref _dirs, value);
    }

    private ObservableCollectionWithSelectedItem<Tag> _tags = new();
    public ObservableCollectionWithSelectedItem<Tag> Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }

    #endregion

    #region NestedProperties

    private bool _isExpanded;
    [JsonIgnore]
    public bool IsExpanded
    {
        get => _isExpanded;
        set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
    }

    private bool _isSelected;
    [JsonIgnore]
    public bool IsSelected
    {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }

    #endregion

    #region Properties

    #endregion

    #region Ctor

    public Dir()
    {
    }

    public Dir(string name, string? link = null, string? note = null)
    {
        _name = name;
        _link = link;
        _note = note;
    }

    #endregion

    #region Funcs

    public void AddChildDir(Dir dir)
    {
        Dirs.Add(dir);
        dir.ParentDir = this;
    }

    public void AddChildDirs(IEnumerable<Dir> dirs)
    {
        foreach (var x in dirs)
            AddChildDir(x);
    }

    public void AddTag(Tag tag)
    {
        Tags.Add(tag);
        tag.Dirs.Add(this);
    }

    public void AddTags(IEnumerable<Tag> tags)
    {
        foreach (var x in tags)
            AddTag(x);
    }

    #endregion
}
