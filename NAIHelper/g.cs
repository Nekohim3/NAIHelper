using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NAIHelper.Utils;
using NAIHelper.Utils.Page;
using NAIHelper.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace NAIHelper;

public static class g
{
    public static Settings    Settings                { get; set; } = new();
    public static RestClient  ApiClient               { get; set; }
    public static RestClient  DanbooruClient          { get; set; }
    public static RestClient  CdnDanbooruClient       { get; set; }
    public static RestClient  GelbooruClient          { get; set; }
    public static RestClient  CdnGelbooruClient       { get; set; }
    public static TagTree     TagTree                 { get; set; }
    public static PageManager PageManager             { get; set; }
    public static bool        WebApiConfigured        { get; set; }
    public static bool        ExternalLinksConfigured { get; set; }
    public static bool        LocalPathsConfigured    { get; set; }

    static g()
    {
        LoadSettings();
        InitClients();

        TagTree = new TagTree();
        //Task.Run(() => TagTree.LoadTags());
    }

    public static void LoadSettings()
    {
        if (!File.Exists("Config.json"))
        {
            Settings.RestoreDefaults();
            File.WriteAllText("Config.json", JsonConvert.SerializeObject(Settings));
        }
        else
        {
            JsonConvert.PopulateObject(File.ReadAllText("Config.json"), Settings);
            //Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Config.json"));
        }
    }

    public static void SaveSettings()
    {
        File.WriteAllText("Config.json", JsonConvert.SerializeObject(Settings));
        InitClients();
    }

    public static void InitClients()
    {
        ApiClient         = new RestClient(new RestClientOptions($"{Settings.ApiAddress}:{Settings.ApiPort}")).UseNewtonsoftJson();
        DanbooruClient    = new RestClient(new RestClientOptions(Settings.DanbooruMainBaseAddress)).UseNewtonsoftJson();
        CdnDanbooruClient = new RestClient(new RestClientOptions(Settings.DanbooruCdnBaseAddress)).UseNewtonsoftJson();
        GelbooruClient    = new RestClient(new RestClientOptions(Settings.GelbooruMainBaseAddress)).UseNewtonsoftJson();
        CdnGelbooruClient = new RestClient(new RestClientOptions(Settings.GelbooruCdnBaseAddress)).UseNewtonsoftJson();
    }
}
