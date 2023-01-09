using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils;
using NAIHelper.ViewModels.UI_Entities;
using RestSharp;

namespace NAIHelper.Services
{
    public abstract class ManyToManyService<T, T1, T2> where T1 : IdEntity where T2 : IdEntity where T : ManyToManyEntity<T1, T2>
    {
        protected readonly string ApiPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiPath">Path to entity api</param>
        protected ManyToManyService(string apiPath)
        {
            ApiPath = apiPath;
        }

        public async Task<List<T>> Get()
        {
            var eList = await g.Client.GetAsync<List<T>>(new RestRequest(ApiPath));
            if (eList != null)
                foreach (var x in eList)
                    x.CreateHash();
            return eList;
        }

        public async Task<bool> Delete(T t) => await g.Client.DeleteAsync<bool>(t.GetTRequest(ApiPath));
    }
}
