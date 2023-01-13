using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using NAIHelper.ViewModels;
using NAIHelper.Views;
using ReactiveUI;

namespace NAIHelper.Utils.Pages
{
    public class PageManager : ViewModelBase
    {
        private ObservableCollectionWithSelectedItem<TPage> _pageList = new();
        public ObservableCollectionWithSelectedItem<TPage> PageList
        {
            get => _pageList;
            set => this.RaiseAndSetIfChanged(ref _pageList, value);
        }

        private MainWindowViewModel _vm;

        public PageManager(MainWindowViewModel vm)
        {
            _vm = vm;
        }

        public void Switch(TPage page)
        {
            foreach (var x in PageList)
                x.IsSelected = false;
            PageList.SelectedItem            = page;
            PageList.SelectedItem.IsSelected = true;
            _vm.CurrentPage                      = PageList.SelectedItem;
        }
    }
}
