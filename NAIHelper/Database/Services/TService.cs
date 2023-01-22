using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.Utils.Extensions;
using Newtonsoft.Json;
using RestSharp;

namespace NAIHelper.Database.Services;

public abstract class TService<T> where T : IdEntity
{
    protected readonly string ApiPath;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiPath">Path to entity api</param>
    protected TService(string apiPath)
    {
        ApiPath = apiPath;
    }

    public virtual async Task<T?> Get(int id)
    {
        var e = await g.ApiClient.GetAsync<T>(new RestRequest($"{ApiPath}/{id}"));
        return e;
    }

    public virtual async Task<List<T>> Get()
    {
        var eList = await g.ApiClient.GetAsync<List<T>>(new RestRequest(ApiPath));
        return eList;
    }

    public virtual async Task<T?> Create(T t)
    {
        var e = await g.ApiClient.PostAsync<T>(t.GetTRequest(ApiPath));
        return null;
    }

    public virtual async Task<List<T>?> Create(List<T> tList)
    {
        try
        {
            var eList = await g.ApiClient.PostAsync<List<T>>(tList.GetTRequest($"{ApiPath}/Bulk"));
            return eList;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public virtual async Task<T?> Update(T t)
    {
        var e = await g.ApiClient.PutAsync<T>(t.GetTRequest(ApiPath));
        return e;
    }

    public virtual async Task<List<T>?> Update(List<T> tList)
    {
        return await g.ApiClient.PutAsync<List<T>>(tList.GetTRequest($"{ApiPath}/Bulk"));
    }

    public virtual async Task<bool> Delete(int id) => await g.ApiClient.DeleteAsync<bool>(new RestRequest($"{ApiPath}/{id}"));
    public virtual async Task<bool> Delete(List<int> tList) => await g.ApiClient.DeleteAsync<bool>(tList.GetTRequest($"{ApiPath}/Bulk"));

    public virtual async Task<T?> Save(T t)
    {
        var e = await g.ApiClient.PatchAsync<T>(t.GetTRequest(ApiPath));
        return e;
    }

    public virtual async Task<List<T>?> Save(List<T> tList)
    {
        var eList = await g.ApiClient.PatchAsync<List<T>>(tList.GetTRequest($"{ApiPath}/Bulk"));
        return eList;
    }
}
