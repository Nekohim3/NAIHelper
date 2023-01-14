using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Database.UI_Entities.BaseEntities;

namespace NAIHelper.Database.UI_Entities;

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
