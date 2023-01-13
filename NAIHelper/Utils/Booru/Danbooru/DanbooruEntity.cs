 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Newtonsoft.Json;

 namespace NAIHelper.Utils.Booru.Danbooru
{
    public class DanbooruEntity : BooruEntity
    {
        [JsonProperty("id")] public override int    Id   { get; set; }
        [JsonProperty("tag_string")] public override                      string Tags { get; set; }
        [JsonProperty("file_ext")] public override                      string Ext  { get; set; }
        [JsonProperty("file_url")] public override                      string Uri  { get; set; }
        //public int Id { get; set; }
        //public string Tag_string { get; set; }
        //public string File_ext { get; set; }
        //public string File_url { get; set; }
    }
}
