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
}
