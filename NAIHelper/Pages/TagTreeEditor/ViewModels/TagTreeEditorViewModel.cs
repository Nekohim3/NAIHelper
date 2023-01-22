using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using NAIHelper.Database.UI_Entities;
using NAIHelper.Utils;
using NAIHelper.Utils.Controls;
using ReactiveUI;

namespace NAIHelper.ViewModels
{
    public class TagTreeEditorViewModel : ViewModelBase
    {
        //private TagTree _tagTree;
        //public TagTree TagTree
        //{
        //    get => _tagTree;
        //    set => this.RaiseAndSetIfChanged(ref _tagTree, value);
        //}

        //private bool _showInnerTags;
        //public bool ShowInnerTags
        //{
        //    get => _showInnerTags;
        //    set
        //    {
        //        this.RaiseAndSetIfChanged(ref _showInnerTags, value); 
        //        LoadTags();
        //    }
        //}
        private TagTreeViewModel _tagTreeViewModel;
        public TagTreeViewModel TagTreeViewModel
        {
            get => _tagTreeViewModel;
            set => this.RaiseAndSetIfChanged(ref _tagTreeViewModel, value);
        }
        public TagTreeEditorViewModel()
        {
            TagTreeViewModel = new TagTreeViewModel(true, true, true);
            //TagTree = g.TagTree;
            //TagTree.RootDirs.SelectionChanged += RootDirsOnSelectionChanged;
        }

        //private void RootDirsOnSelectionChanged(ObservableCollectionWithSelectedItem<Dir> sender, Dir newselection, Dir oldselection)
        //{
        //    if(TagTree.RootDirs.SelectedItem != null)
        //        LoadTags();
        //}

        //public void TagsOnSelectionChanged(Tag newselection, Tag oldselection)
        //{
        //    //OnCancelTag();
        //}

        //public void LoadTags()
        //{
        //    if (g.TagTree.RootDirs.SelectedItem == null) return;
        //    g.TagTree.Tags.Clear();
        //    var tags = new List<Tag>();
        //    if (ShowInnerTags)
        //        LoadTags(tags, g.TagTree.RootDirs.SelectedItem);
        //    else
        //        tags.AddRange(g.TagTree.RootDirs.SelectedItem.Tags);
        //    g.TagTree.Tags.AddRange(tags.OrderBy(_ => _.Name).Distinct());
        //}

        //private void LoadTags(List<Tag> tags, Dir dir)
        //{
        //    tags.AddRange(dir.Tags);
        //    foreach (var x in dir.Dirs) LoadTags(tags, x);
        //}


    }
}
