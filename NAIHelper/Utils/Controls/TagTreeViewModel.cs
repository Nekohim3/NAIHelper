using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using NAIHelper;
using NAIHelper.Database.UI_Entities;
using NAIHelper.Utils;
using NAIHelper.ViewModels;
using ReactiveUI;

namespace NAIHelper.Utils.Controls
{
    public class TagTreeViewModel : ViewModelBase
    {
        public TagTree _tagTree;
        public TagTree TagTree
        {
            get => _tagTree;
            set => this.RaiseAndSetIfChanged(ref _tagTree, value);
        }

        private bool _showInnerTags;
        public bool ShowInnerTags
        {
            get => _showInnerTags;
            set
            {
                this.RaiseAndSetIfChanged(ref _showInnerTags, value);
                LoadTags();
            }
        }

        private Dir _selectedDir;
        public Dir SelectedDir
        {
            get => _selectedDir;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDir, value); 
                LoadTags();
            }
        }

        private bool _useTagDirs;
        public bool UseTagDirs
        {
            get => _useTagDirs;
            set => this.RaiseAndSetIfChanged(ref _useTagDirs, value);
        }

        public TagTreeViewModel()
        {

        }

        public TagTreeViewModel(bool useTagDirs, bool useSearch, bool useEdit)
        {
            TagTree                           =  g.TagTree;
            TagTree.RootDirs.SelectionChanged += RootDirsOnSelectionChanged;
            _useTagDirs                       =  useTagDirs;
        }

        private void RootDirsOnSelectionChanged(ObservableCollectionWithSelectedItem<Dir> sender, Dir newselection, Dir oldselection)
        {
            if (TagTree.RootDirs.SelectedItem != null)
                LoadTags();
        }

        public void DirsOnSelectionChanged(Dir newselection, Dir oldselection)
        {
            LoadTags();
            //OnCancelTag();
        }
        public void TagsOnSelectionChanged(Tag newselection, Tag oldselection)
        {
            //OnCancelTag();
        }

        public void LoadTags()
        {
            if (SelectedDir == null) return;
            g.TagTree.Tags.Clear();
            var tags = new List<Tag>();
            if (ShowInnerTags)
                LoadTags(tags, SelectedDir);
            else
                tags.AddRange(SelectedDir.Tags);
            g.TagTree.Tags.AddRange(tags.OrderBy(_ => _.Name).Distinct());
        }

        private void LoadTags(List<Tag> tags, Dir dir)
        {
            tags.AddRange(dir.Tags);
            foreach (var x in dir.Dirs) LoadTags(tags, x);
        }
    }
}
