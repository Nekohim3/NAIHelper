using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIHelper.Utils.Booru;

public class BooruEntity
{
    public virtual int    Id            { get; set; }
    public virtual string Tags          { get; set; }
    public virtual string Ext           { get; set; }
    public virtual string Uri           { get; set; }
    public         string FormattedTags => Tags.Replace(" ", ",").Replace("_", " ");
    public         string UriPath       => new Uri(Uri).AbsolutePath;
}
