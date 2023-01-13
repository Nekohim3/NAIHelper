using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using NAIHelper.Utils.Interfaces;
using NAIHelper.ViewModels;
using ReactiveUI;

namespace NAIHelper.Utils.Pages
{
    public class TPage : ViewModelBase, ISelected
    {
        private UserControl _page;
        public UserControl Page
        {
            get => _page;
            set => this.RaiseAndSetIfChanged(ref _page, value);
        }

        private ViewModelBase _viewModel;
        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value);
        }

        public ReactiveCommand<Unit, Unit> SwitchCmd { get; }

        public TPage()
        {
            
        }

        public TPage(UserControl page, ViewModelBase viewModel, string name)
        {
            SwitchCmd        = ReactiveCommand.Create(OnSwitch);
            _page            = page;
            _viewModel       = viewModel;
            page.DataContext = viewModel;
            _name            = name;
        }

        private void OnSwitch()
        {
            g.PageManager.Switch(this);
        }
    }
}
