using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NAIHelper.Utils.Extensions;
using NAIHelper.ViewModels.UI_Entities;
using OpenQA.Selenium.Firefox;
using RestSharp;

namespace NAIHelper.Services;

public class DirService : TService<Dir>
{
    public DirService() : base("Dirs")
    {
    }

    public override async Task<List<Dir>?> Create(List<Dir> tList)
    {
        var eList = await g.ApiClient.PostAsync<List<Dir>>(tList.GetTRequest($"{ApiPath}/Bulk"));
        //foreach (var x in eList)
            //CreateHashRecursive(x);
        return eList;
    }

    public async Task<bool> UnlinkTag(Dir dir, Tag tag)
    {
        var dtService = new DirTagService();
        var res       = await dtService.Delete(new DirTag(dir.Id, tag.Id));
        return res;
    }

    //private void CreateHashRecursive(Dir dir)
    //{
    //    dir.CreateHash();
    //    foreach (var x in dir.Tags)
    //        x.CreateHash();

    //    foreach (var x in dir.Dirs)
    //        CreateHashRecursive(x);
    //}
}
