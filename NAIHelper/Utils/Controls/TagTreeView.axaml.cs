using System.Collections;
using Avalonia.Controls;
using NAIHelper.ViewModels;
using System.Linq;
using Avalonia.Input;
using NAIHelper.Database.UI_Entities;

namespace NAIHelper.Utils.Controls
{
    public partial class TagTreeView : UserControl
    {
        public TagTreeView()
        {
            InitializeComponent();
        }

        private void TagListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as TagTreeViewModel;
            vm.TagsOnSelectionChanged((e.AddedItems as object[]).OfType<Tag>().FirstOrDefault(), (e.RemovedItems as object[]).OfType<Tag>().FirstOrDefault());
        }

        //private void TreeView_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        //{
        //    var vm = DataContext as TagTreeViewModel;
        //    vm.DirsOnSelectionChanged((e.AddedItems as IList).OfType<Dir>().FirstOrDefault(), (e.RemovedItems as object[]).OfType<Dir>().FirstOrDefault());
        //}
        
    }
}
