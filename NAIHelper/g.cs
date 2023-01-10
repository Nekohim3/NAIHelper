using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NAIHelper.Services;
using NAIHelper.Utils;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace NAIHelper
{
    public class g
    {
        public static Formatting            Formatting { get; set; } = Formatting.Indented;
        public static RestClient            Client = new RestClient(new RestClientOptions("http://localhost:5022")).UseNewtonsoftJson();
        public static Driver                Driver          { get; set; }
    }
}
