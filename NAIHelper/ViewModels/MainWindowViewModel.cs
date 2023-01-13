using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAIHelper.Utils;
using NAIHelper.Utils.Pages;
using NAIHelper.Views;
using ReactiveUI;

namespace NAIHelper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private TPage _currentPage;
        public TPage CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        private PageManager _pageManager;
        public PageManager PageManager
        {
            get => _pageManager;
            set => this.RaiseAndSetIfChanged(ref _pageManager, value);
        }

        public MainWindowViewModel()
        {
            g.PageManager = new PageManager(this);
            PageManager   = g.PageManager;
            g.PageManager.PageList.Add(new TPage(new TagTreeEditorView(), new TagTreeEditorViewModel(), "Tag editor"));
            g.PageManager.PageList.Add(new TPage(new TestView(),          null,                         "Test view"));
            g.PageManager.PageList.Add(new TPage(new TestView(),          null,                         "Test view"));
            g.PageManager.PageList.Add(new TPage(new TestView(),          null,                         "Test view"));
            g.PageManager.PageList.Add(new TPage(new TestView(),          null,                         "Test view"));
            g.PageManager.PageList.Add(new TPage(new TestView(),          null,                         "Test view"));
            g.PageManager.PageList.Add(new TPage(new TestView(),          null,                         "Test view"));
            g.PageManager.Switch(g.PageManager.PageList.First());
        }
    }
}
