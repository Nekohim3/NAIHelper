using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using NAIHelper.Database.Services;
using NAIHelper.Pages.Lora.ViewModels;
using NAIHelper.Pages.Lora.Views;
using NAIHelper.Pages.PromptEditor.ViewModels;
using NAIHelper.Pages.PromptEditor.Views;
using NAIHelper.Utils;
using NAIHelper.Utils.Booru;
using NAIHelper.Utils.Extensions;
using NAIHelper.Utils.Page;
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

        private Settings _settings;
        public Settings Settings
        {
            get => _settings;
            set => this.RaiseAndSetIfChanged(ref _settings, value);
        }

        private bool _wrongSettingsDetected;
        public bool WrongSettingsDetected
        {
            get => _wrongSettingsDetected;
            set => this.RaiseAndSetIfChanged(ref _wrongSettingsDetected, value);
        }

        private bool _loadingAPI;
        public bool LoadingAPI
        {
            get => _loadingAPI;
            set => this.RaiseAndSetIfChanged(ref _loadingAPI, value);
        }

        private bool _showConfig;
        public bool ShowConfig
        {
            get => _showConfig;
            set => this.RaiseAndSetIfChanged(ref _showConfig, value);
        }

        public ReactiveCommand<Unit, Unit> ShowConfigCmd { get; }
        public ReactiveCommand<Unit, Unit> SaveConfigCmd { get; }
        public ReactiveCommand<Unit, Unit> CancelConfigCmd { get; }
        public ReactiveCommand<Unit, Unit> RestoreConfigCmd { get; }
        public ReactiveCommand<Unit, Unit> TestCmd { get; }

        public ReactiveCommand<Unit, Unit> ResetDatabaseCmd { get; }


        
        
        public MainWindowViewModel()
        {
            ShowConfigCmd    = ReactiveCommand.Create(OnShowConfig, this.WhenAnyValue(_ => _.LoadingAPI).Select(_ => !_));
            RestoreConfigCmd = ReactiveCommand.Create(OnRestoreConfig);
            CancelConfigCmd  = ReactiveCommand.Create(OnCancelConfig);
            SaveConfigCmd    = ReactiveCommand.Create(OnSaveConfig);
            TestCmd          = ReactiveCommand.Create(OnTest);
            ResetDatabaseCmd = ReactiveCommand.Create(OnResetDatabase);
            Settings         = g.Settings;
            g.PageManager    = new PageManager(this);
            PageManager      = g.PageManager;
            //Task.Run(ValidateApiConnection);
            ValidateApiConnection();
        }

        public async void ValidateApiConnection()
        {
            LoadingAPI            = true;
            WrongSettingsDetected = false;
            var service = new SetupService();
            var res     = await service.SetupContext();
            if (res.success)
            {
                g.WebApiConfigured    = true;
                InitTabs();
                await Task.Run(() => g.TagTree.Load());
            }
            else
            {
                DeInitTabs();
                await g.TagTree.ClearData();
                WrongSettingsDetected = true;
            }
            LoadingAPI = false;
        }

        public void InitTabs()
        {
            DeInitTabs();
            PageManager.PageList.Add(new TPage(new PromptEditorView(),          new PromptEditorViewModel(),                         "Prompt editor"));
            PageManager.PageList.Add(new TPage(new TagTreeEditorView(), new TagTreeEditorViewModel(), "Tag editor"));
            PageManager.PageList.Add(new TPage(new LoraView(),          new LoraViewModel(),          "LORA"));
            PageManager.Switch(g.PageManager.PageList.First());
        }

        public void DeInitTabs()
        {
            PageManager.PageList.Clear();
            CurrentPage = null;
        }

        private void OnShowConfig()
        {
            ShowConfig = !ShowConfig;
        }

        private void OnSaveConfig()
        {
            g.SaveSettings();
            ShowConfig = false;
            ValidateApiConnection();
        }

        private void OnCancelConfig()
        {
            g.LoadSettings();
            ShowConfig = false;
        }

        private void OnRestoreConfig()
        {
            Settings.RestoreDefaults();
        }

        private async void OnResetDatabase()
        {
            var service = new SetupService();
            await service.EnsureDeleted();
        }

        private async void OnTest()
        {
            //var btl     = new BooruTagsLoader();
            //var tree    = await btl.DownloadTree();
            //var service = new DirService();
            //var res     = await service.Create(tree);
            var q = g.TagTree.RootDirs[1].FromTree(_ => _.Dirs).ToList();
        }
    }
}
