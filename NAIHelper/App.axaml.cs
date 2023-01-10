using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NAIHelper.Services;
using NAIHelper.Utils;
using NAIHelper.Utils.Extensions;
using NAIHelper.ViewModels;
using NAIHelper.ViewModels.UI_Entities;
using NAIHelper.Views;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;

namespace NAIHelper;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings() {MaxDepth = 1024, TypeNameHandling = TypeNameHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects, NullValueHandling = NullValueHandling.Ignore};
        //var dService  = new DirService();
        ////var tService  = new TagService();
        ////var dtService = new DirTagService();
        //var dts = new DirTagService();
        //var q   = await dts.Get();
        //var dirs = (await dService.Get()).StartTracking();
        ////dirs.StartTracking();
        //var old = dirs[0].Name;
        //var c1 = dirs[0].IsChanged;
        //dirs[0].Name = "dfg";
        //var c2 = dirs[0].IsChanged;
        //dirs[0].RevertChanges();
        ////dirs[0].Name = old;
        //var c3 = dirs[0].IsChanged;
        //var tags      = await tService.Get();
        ////var dt        = new DirTag(dirs[1], tags[0]);
        ////var q         = JsonConvert.SerializeObject(dt);
        //var res1      = await dService.UnlinkTag(dirs[1], tags[0]);
        //service.Update(dirs);
        //var dir1 = new Dir("d1");
        //var dir2 = new Dir("d2");
        //var tag1 = new Tag("t1");
        //var tag2 = new Tag("t2");

        //dir1.AddChildDir(dir2);
        //dir1.Tags.Add(tag1);
        //dir2.Tags.Add(tag1);
        //dir2.Tags.Add(tag2);

        //tag1.Dirs.Add(dir1);
        //tag1.Dirs.Add(dir2);
        //tag2.Dirs.Add(dir2);
        //var qq      = JsonConvert.SerializeObject(dir1, Formatting.Indented);
        //var service = new DirService();
        //var q       = await service.Create(new List<Dir>(){dir1});

        //var service = new DirTagService();
        //var list    = await service.Get();

        //new DanbooruDownloader().DownloadFromFile();
        var t = new TagTree();
        t.LoadTags();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow {DataContext = new MainWindowViewModel()};
        }

        base.OnFrameworkInitializationCompleted();
    }
}
