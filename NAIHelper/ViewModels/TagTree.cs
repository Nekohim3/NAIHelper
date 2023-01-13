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

    private Dictionary<int, Dir> _dirList      = new();
    private Dictionary<int, Tag> _tagList      = new();
    private Dictionary<int, Session>  _sessionList  = new();
    private Dictionary<int, Group>    _groupList    = new();
    private Dictionary<int, GroupTag> _groupTagList = new();

    private DirService      _dirService      = new();
    private TagService      _tagService      = new();
    private DirTagService   _dirTagService   = new();
    private SessionService  _sessionService  = new();
    private GroupService    _groupService    = new();
    private GroupTagService _groupTagService = new();

    private int       _selectedId;
    private int       _selectedSessionId;
    private List<int> _expandedIds = new();

    public TagTree()
    {
    }

    public void Load(bool remember = false)
    {
        LoadTags(remember);
        LoadSessions(remember);
    }

    public async void LoadTags(bool remember = false)
    {
        RootDirs.Clear();
        Tags.Clear();
        SearchedTags.Clear();
        _dirList = (await _dirService.Get()).ToDictionary(_ => _.Id);
        _tagList = (await _tagService.Get()).ToDictionary(_ => _.Id);
        var dirTagList = await _dirTagService.Get();
        foreach (var x in _dirList)
        {
            if (x.Value.IdParent.HasValue)
                _dirList[x.Value.IdParent.Value].AddChildDir(x.Value);
            else
                RootDirs.Add(x.Value);
            foreach (var c in dirTagList.Where(v => v.IdFirst == x.Value.Id))
                x.Value.Tags.Add(_tagList[c.IdSecond]);
        }

        foreach (var x in _tagList)
        foreach (var c in dirTagList.Where(v => v.IdSecond == x.Value.Id))
            x.Value.Dirs.Add(_dirList[c.IdFirst]);
    }

    public async void LoadSessions(bool remember = false)
    {
    }

    public void RememberExpandedAndSelectedDirsTags()
    {
        _expandedIds = _dirList.Where(x => x.Value.IsExpanded && x.Value.Id > 0).Select(x => x.Value.Id).ToList();
        _selectedId  = RootDirs.SelectedItem?.Id ?? 0;
    }

    public void RestoreExpandedAndSelectedDirsTags()
    {
        foreach (var x in _dirList.Where(x => _expandedIds.Contains(x.Value.Id)))
            x.Value.IsExpanded = true;
        RootDirs.SelectedItem = _dirList.FirstOrDefault(x => x.Value.Id == _selectedId).Value;
    }
}
