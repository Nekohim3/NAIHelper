using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils.Extensions;
using NAIHelper.ViewModels.UI_Entities;
using NAIHelper.ViewModels.UI_Entities.BaseEntities;
using RestSharp;

namespace NAIHelper.Services;

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

    public virtual async Task<List<T>> Get()
    {
        var eList = await g.ApiClient.GetAsync<List<T>>(new RestRequest(ApiPath));
        //if (eList != null)
        //    foreach (var x in eList)
        //        x.CreateHash();
        return eList;
    }

    public virtual async Task<List<T>> GetByFirst(int id)
    {
        var eList = await g.ApiClient.GetAsync<List<T>>(new RestRequest($"{ApiPath}/ByFirst/{id}"));
        //if (eList != null)
        //    foreach (var x in eList)
        //        x.CreateHash();
        return eList;
    }

    public virtual async Task<List<T>> GetBySecond(int id)
    {
        var eList = await g.ApiClient.GetAsync<List<T>>(new RestRequest($"{ApiPath}/BySecond/{id}"));
        //if (eList != null)
        //    foreach (var x in eList)
        //        x.CreateHash();
        return eList;
    }

    public virtual async Task<T> GetByBoth(int idFirst, int idSecond)
    {
        var e = await g.ApiClient.GetAsync<T>(new RestRequest($"{ApiPath}/ByBoth/{idFirst}/{idSecond}"));
        //if (e != null)
        //    e.CreateHash();
        return e;
    }

    public virtual async Task<bool> Delete(T t) => await g.ApiClient.DeleteAsync<bool>(t.GetTRequest(ApiPath));
}
