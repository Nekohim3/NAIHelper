using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace NAIHelper.Utils.Extensions
{
    public static class RestRequestExtensions
    {
        public static RestRequest GetTRequest(this object obj, string uri, Method m = Method.Get)
        {
            var req = new RestRequest(uri, m);
            req.AddStringBody(JsonConvert.SerializeObject(obj), DataFormat.Json);
            return req;
        }

        public static string GetJson(this object obj) => JsonConvert.SerializeObject(obj);
    }
}
