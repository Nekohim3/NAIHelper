using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using NAIHelper.Database.Services;
using NAIHelper.Database.UI_Entities;
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
        //var q       = await service.Create(new List<Dir>(){dir1});

        //var service = new DirTagService();
        //var list    = await service.Get();
        //var src = "https://cdn.donmai.us/original/93/37/__raiden_shogun_and_yae_miko_genshin_impact_drawn_by_niliu_chahui__9337e38d12eb7df6f3b227fe01fa8b38.jpg";
        //var req = new RestRequest("posts.json", Method.Post);
        //req.AddJsonBody("{\"search\":{\"tag_string_artist\":\"niliu_chahui\"}, \"_method\":\"post\"}");
        //var res = g.DanbooruClient.Execute(req);



        //var files      = Directory.GetFiles("C:\\NAI\\Datasets\\niliu_chahui\\training_data\\images\\10_niliu_chahui").Where(_ => System.IO.Path.GetExtension(_) == ".txt");
        //var lst        = new List<(int count, string tags)>();
        //var maxCounter = 0;
        //var maxFile    = "";
        //foreach (var x in files)
        //{
        //    var tags    = File.ReadAllText(x);
        //    var c1      = tags.Count(_ => _ == ',');
        //    var c2      = tags.Split(new[] { ",", " " }, StringSplitOptions.None).Length;
        //    var counter = c1 + c2;
        //    lst.Add((counter, tags));
        //    if (counter > 180)
        //    {
        //        File.Delete($"C:\\NAI\\Datasets\\niliu_chahui\\training_data\\images\\10_niliu_chahui\\{Path.GetFileNameWithoutExtension(x)}.txt");
        //        try
        //        {
        //            File.Delete($"C:\\NAI\\Datasets\\niliu_chahui\\training_data\\images\\10_niliu_chahui\\{Path.GetFileNameWithoutExtension(x)}.jpg");
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //        try
        //        {
        //            File.Delete($"C:\\NAI\\Datasets\\niliu_chahui\\training_data\\images\\10_niliu_chahui\\{Path.GetFileNameWithoutExtension(x)}.png");
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }
        //    if (counter > maxCounter)
        //    {
        //        maxCounter = counter;
        //        maxFile    = x;
        //    }
        //}

        //lst = lst.OrderByDescending(_ => _.count).ToList();
        //var llst = lst.Select(_ => _.tags).ToList();
        //var stream = g.BooruClient.DownloadStream(req);
        //var buf = new byte[stream.Length];
        //stream.Read(buf);
        //using var fs  = new FileStream("C:\\NAI\\Datasets\\test.jpg", FileMode.Create, FileAccess.Write);
        //fs.Write(buf);
        //var       res = g.BooruClient.Execute(req);
        //var       str = res.Content;
        //https://cdn.donmai.us/original/93/37/__raiden_shogun_and_yae_miko_genshin_impact_drawn_by_niliu_chahui__9337e38d12eb7df6f3b227fe01fa8b38.jpg?download=1
        //t.LoadTags();



        //var q = ImageCompare("C:\\NAI\\Datasets\\niliu_chahui\\training_data\\images\\n_niliu_chahui\\5973742.jpg",
        //"C:\\NAI\\Datasets\\niliu_chahui\\training_data\\images\\n_niliu_chahui\\5973748.jpg");
        //await new DanbooruDownloader().DownloadConcept("niliu_chahui");

        //await new BooruDownloader().DownloadConcept("niliu_chahui", Booru.Gelbooru);
        //new BooruDownloader().DownloadFromSite();

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
