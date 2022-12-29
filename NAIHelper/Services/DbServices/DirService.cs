using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Models;
using Newtonsoft.Json;

namespace NAIHelper.Services.DbServices
{
    public class DirService<T> : TService<T> where T : IdEntity, new()
    {
        public string GetAll() => JsonConvert.SerializeObject(GetRange(), new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.Arrays, Formatting = g.Formatting});

        public string SaveAll(List<T> tList) => JsonConvert.SerializeObject(SaveRange(tList));

        public string DeleteAll(List<T> tList) => JsonConvert.SerializeObject(DeleteRange(tList));
    }
}
