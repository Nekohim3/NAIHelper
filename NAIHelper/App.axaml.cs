using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using NAIHelper.Database.Services;
using NAIHelper.Database.UI_Entities;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils;
using NAIHelper.Utils.Booru;
using NAIHelper.Utils.Extensions;
using NAIHelper.ViewModels;
using NAIHelper.Views;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;
using SkiaSharp;
using Path = System.IO.Path;

namespace NAIHelper;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        g.DanbooruClient.Authenticator = new HttpBasicAuthenticator(g.Settings.DanbooruUsername, g.Settings.DanbooruApiKey);
        g.CdnDanbooruClient.Authenticator = new HttpBasicAuthenticator(g.Settings.DanbooruUsername, g.Settings.DanbooruApiKey);
        g.GelbooruClient.Options.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 OPR/93.0.0.0";
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings() { MaxDepth = 1024, TypeNameHandling = TypeNameHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects };
        //var setupService = new SetupService();
        //setupService.SetupContext();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        #region Test

        var dir = new Dir("qwe");
        dir.StartTracking();
        var changed1 = dir.IsPropertyChanged(_ => _.Name);
        var c1       = dir.IsChanged;
        dir.Name = "asd";
        var changed2 = dir.IsPropertyChanged(_ => _.Name);
        dir.Name = "qwe";
        var changed3  = dir.IsPropertyChanged(_ => _.Name);
        var changed21 = dir.IsPropertyChanged(_ => _.Name);
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

        //var lst = dir1.FromTree(_ => _.Dirs);
        //var qq      = JsonConvert.SerializeObject(dir1, Formatting.Indented);
        //var service = new DirService();
        //var q       = await service.Create(dir1);

        //var service = new DirTagService();
        //var list    = await service.Get();
        //var req = new RestRequest("posts.json", Method.Post);
        //var res = g.DanbooruClient.Execute(req);


        
        //new BooruDownloader().DownloadFromSite();
        //var bl      = new BooruTagsLoader();
        //var tree    = await bl.DownloadTree();
        //var qq      = JsonConvert.SerializeObject(tree, Formatting.Indented);
        //var service = new DirService();
        //var q       = await service.Create(tree);
        #endregion

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel() };
        }
        base.OnFrameworkInitializationCompleted();
    }

    public int ImageCompare(string path1, string path2)
    {
        var bytes1 = SKBitmap.Decode(File.OpenRead(path1)).Bytes;
        var bytes2 = SKBitmap.Decode(File.OpenRead(path2)).Bytes;
        if (bytes1.LongLength != bytes2.LongLength)
            return 0;
        var counter = 0;
        for (var i = 0; i < bytes1.LongLength; i++)
        {
            if (bytes1[i] != bytes2[i])
                counter++;
        }
        return (int)Math.Round((double)counter / bytes1.LongLength * 100, 0);
    }
}
