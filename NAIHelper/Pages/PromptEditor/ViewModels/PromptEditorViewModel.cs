using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils.Controls;
using NAIHelper.ViewModels;
using ReactiveUI;

namespace NAIHelper.Pages.PromptEditor.ViewModels
{
    public class PromptEditorViewModel : ViewModelBase
    {
        private TagTreeViewModel _tagTreeViewModel;
        public TagTreeViewModel TagTreeViewModel
        {
            get => _tagTreeViewModel;
            set => this.RaiseAndSetIfChanged(ref _tagTreeViewModel, value);
        }

        public PromptEditorViewModel()
        {
            TagTreeViewModel = new TagTreeViewModel(false, true, false);
        }
    }
}
