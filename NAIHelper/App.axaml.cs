using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NAIHelper.Models;
using NAIHelper.Utils;
using NAIHelper.ViewModels;
using NAIHelper.ViewModels.UI_Entities;
using NAIHelper.Views;

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
            var q1 = new UI_Dir("qwe");
            var q2 = new UI_Dir("qwe");
            var q3 = new UI_Dir("asd");
            var w1 = q1.GetHashCode();
            var w2 = q2.GetHashCode();
            var w3 = q3.GetHashCode();
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
