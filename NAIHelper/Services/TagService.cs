using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.ViewModels.UI_Entities;

namespace NAIHelper.Services;

public class TagService : TService<Tag>
{
    public TagService() : base("Tags")
    {
    }

    public async Task<bool> UnlinkDir(Dir dir, Tag tag)
    {
        var dtService = new DirTagService();
        var res       = await dtService.Delete(new DirTag(dir.Id, tag.Id));
        return res;
    }
}
