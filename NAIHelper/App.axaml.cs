using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NAIHelper.Models;
using NAIHelper.Services.DbServices;
using NAIHelper.Utils;
using NAIHelper.ViewModels;
using NAIHelper.ViewModels.UI_Entities;
using NAIHelper.Views;
using Newtonsoft.Json;

namespace NAIHelper
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var    str = new DirService<Dir>().GetAll();
            //var lst = JsonConvert.DeserializeObject<Dir>(str, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore});
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
