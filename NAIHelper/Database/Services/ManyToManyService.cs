using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils.Extensions;
using RestSharp;

namespace NAIHelper.Database.Services;

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
        return eList;
    }

    public virtual async Task<List<T>> GetByFirst(int id)
    {
        var eList = await g.ApiClient.GetAsync<List<T>>(new RestRequest($"{ApiPath}/ByFirst/{id}"));
        return eList;
    }

    public virtual async Task<List<T>> GetBySecond(int id)
    {
        var eList = await g.ApiClient.GetAsync<List<T>>(new RestRequest($"{ApiPath}/BySecond/{id}"));
        return eList;
    }

    public virtual async Task<T> GetByBoth(int idFirst, int idSecond)
    {
        var e = await g.ApiClient.GetAsync<T>(new RestRequest($"{ApiPath}/ByBoth/{idFirst}/{idSecond}"));
        return e;
    }

    public virtual async Task<bool> Delete(T t) => await g.ApiClient.DeleteAsync<bool>(t.GetTRequest(ApiPath));
}
