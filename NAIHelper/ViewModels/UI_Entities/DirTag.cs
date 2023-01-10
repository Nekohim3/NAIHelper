using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.ViewModels.UI_Entities.BaseEntities;

namespace NAIHelper.ViewModels.UI_Entities;

public class DirTag : ManyToManyEntity<Dir, Tag>
{
    public DirTag()
    {
    }

    public DirTag(int idFirst, int idSecond) : base(idFirst, idSecond)
    {
    }

    public DirTag(Dir first, Tag second) : base(first, second)
    {
    }
}
