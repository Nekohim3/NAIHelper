using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NAIHelper.Utils.Booru.GelBooru
{
    public class GelbooruEntity : BooruEntity
    {
        [JsonProperty("id")]   public override     int    Id    { get; set; }
        [JsonProperty("tags")] public override     string Tags  { get; set; }
        public override                            string Ext   => Path.GetExtension(Image).Replace(".", "");
        [JsonProperty("image")]    public          string Image { get; set; }
        [JsonProperty("file_url")] public override string Uri   { get; set; }
        //public                                       int    Id       { get; set; }
        //public                                       string Tags     { get; set; }
        //public                                       string File_ext => Path.GetExtension(Image).Replace(".", "");
        //public                                       string File_url { get; set; }
        //public                                       string Image    { get; set; }
    }
    public class GelbooruPosts
    {
        [JsonProperty("post")] public List<GelbooruEntity> PostList { get; set; }
    }
}
