using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NAIHelper.Services;
using NAIHelper.Utils;
using NAIHelper.Utils.Pages;
using NAIHelper.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace NAIHelper;

public static class g
{
    public static Formatting Formatting { get; set; } = Formatting.Indented;
    public static RestClient ApiClient         = new RestClient(new RestClientOptions("http://localhost:5022")).UseNewtonsoftJson();
    public static RestClient DanbooruClient    = new RestClient(new RestClientOptions("https://danbooru.donmai.us")).UseNewtonsoftJson();
    public static RestClient CdnDanbooruClient = new RestClient(new RestClientOptions("https://cdn.donmai.us")).UseNewtonsoftJson();
    public static RestClient GelbooruClient    = new RestClient(new RestClientOptions("https://gelbooru.com")).UseNewtonsoftJson();
    public static RestClient CdnGelbooruClient = new RestClient(new RestClientOptions("https://gelbooru.com")).UseNewtonsoftJson();
    public static TagTree    TagTree { get; set; }
    public static PageManager PageManager { get; set; }

    static g()
    {
        //PageManager = new PageManager();
        TagTree     = new TagTree();
        TagTree.LoadTags();
    }
}
