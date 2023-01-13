using Avalonia.Controls;
using NAIHelper.ViewModels.UI_Entities;
using System.Linq;
using NAIHelper.ViewModels;

namespace NAIHelper.Views
{
    public partial class TagTreeEditorView : UserControl
    {
        public TagTreeEditorView()
        {
            InitializeComponent();
        }

        private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as TagTreeEditorViewModel;
            vm.TagsOnSelectionChanged((e.AddedItems as object[]).OfType<Tag>().FirstOrDefault(), (e.RemovedItems as object[]).OfType<Tag>().FirstOrDefault());
        }
    }
}
