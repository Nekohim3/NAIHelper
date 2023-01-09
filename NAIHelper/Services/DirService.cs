using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NAIHelper.Utils;
using NAIHelper.ViewModels.UI_Entities;
using RestSharp;

namespace NAIHelper.Services;

public class DirService : TService<Dir>
{
    public DirService() : base("Dirs")
    {
    }

    public async Task<bool> UnlinkTag(Dir dir, Tag tag)
    {
        var dtService = new DirTagService();
        var res = await dtService.Delete(new DirTag(dir.Id, tag.Id));
        return res;
    }
}
