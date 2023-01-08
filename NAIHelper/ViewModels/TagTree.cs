using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using NAIHelper.Services;
using NAIHelper.Utils;
using NAIHelper.ViewModels.UI_Entities;
using ReactiveUI;

namespace NAIHelper.ViewModels;

public class TagTree : ViewModelBase
{
    private ObservableCollectionWithSelectedItem<Dir> _rootDirs = new();
    public ObservableCollectionWithSelectedItem<Dir> RootDirs
    {
        get => _rootDirs;
        set => this.RaiseAndSetIfChanged(ref _rootDirs, value);
    }

    private ObservableCollectionWithSelectedItem<Tag> _tags = new();
    public ObservableCollectionWithSelectedItem<Tag> Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }

    private ObservableCollectionWithSelectedItem<Tag> _searchedTags = new();

    public ObservableCollectionWithSelectedItem<Tag> SearchedTags
    {
        get => _searchedTags;
        set => this.RaiseAndSetIfChanged(ref _searchedTags, value);
    }

    private ObservableCollectionWithSelectedItem<Session> _sessions = new();

    public ObservableCollectionWithSelectedItem<Session> Sessions
    {
        get => _sessions;
        set => this.RaiseAndSetIfChanged(ref _sessions, value);
    }

    private List<Dir>      _dirList      = new();
    private List<Tag>      _tagList      = new();
    private List<Session>  _sessionList  = new();
    private List<Group>    _groupList    = new();
    private List<GroupTag> _groupTagList = new();

    private DirService      _dirService      = new();
    private TagService      _tagService      = new();
    private SessionService  _sessionService  = new();
    private GroupService    _groupService    = new();
    private GroupTagService _groupTagService = new();

    private int       _selectedId;
    private int       _selectedSessionId;
    private List<int> _expandedIds = new();

    public TagTree()
    {
    }

    public void LoadAdd(bool remember = false)
    {
        LoadTags(remember);
        LoadSessions(remember);
    }

    public async void LoadTags(bool remember = false)
    {
        RootDirs.Clear();
        Tags.Clear();
        SearchedTags.Clear();
        _dirList = await _dirService.Get();
        _tagList = await _tagService.Get();

        var dirDictId = _dirList.ToDictionary(_ => _.Id);
        foreach (var tag in _tagList)
            dirDictId[tag.IdDir].AddTag(tag);

        foreach (var x in dirDictId)
        {
            if (x.Value.IdParent.HasValue)
                dirDictId[x.Value.IdParent.Value].AddChildDir(x.Value);
            else
                RootDirs.Add(x.Value);
        }
    }

    public async void LoadSessions(bool remember = false)
    {
    }

    public void RememberExpandedAndSelectedDirsTags()
    {
        _expandedIds = _dirList.Where(x => x.IsExpanded && x.Id > 0).Select(x => x.Id).ToList();
        _selectedId  = RootDirs.SelectedItem?.Id ?? 0;
    }

    public void RestoreExpandedAndSelectedDirsTags()
    {
        foreach (var x in _dirList.Where(x => _expandedIds.Contains(x.Id))) x.IsExpanded = true;
        RootDirs.SelectedItem = _dirList.FirstOrDefault(x => x.Id == _selectedId);
    }
}
