using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils.Extensions;
using NAIHelper.ViewModels.UI_Entities;
using NAIHelper.ViewModels.UI_Entities.BaseEntities;
using Newtonsoft.Json;
using RestSharp;

namespace NAIHelper.Services;

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
        var e = await g.Client.GetAsync<T>(new RestRequest($"{ApiPath}/{id}"));
        //e.CreateHash();
        return e;
    }

    public virtual async Task<List<T>> Get()
    {
        var eList = await g.Client.GetAsync<List<T>>(new RestRequest(ApiPath));
        //if (eList != null)
        //    foreach (var x in eList)
        //        x.CreateHash();
        return eList;
    }

    public virtual async Task<T?> Create(T t)
    {
        var e = await g.Client.PostAsync<T>(t.GetTRequest(ApiPath));
        //e.CreateHash();
        return null;
    }

    public virtual async Task<List<T>?> Create(List<T> tList)
    {
        var eList = await g.Client.PostAsync<List<T>>(tList.GetTRequest($"{ApiPath}/Bulk"));
        //foreach (var x in eList)
        //    x.CreateHash();
        return eList;
    }

    public virtual async Task<T?> Update(T t)
    {
        var e = await g.Client.PutAsync<T>(t.GetTRequest(ApiPath));
        //e.CreateHash();
        return e;
    }

    public virtual async Task<List<T>?> Update(List<T> tList)
    {
        return await g.Client.PutAsync<List<T>>(tList.GetTRequest($"{ApiPath}/Bulk"));
    }

    public virtual async Task<bool> Delete(int       id)    => await g.Client.DeleteAsync<bool>(new RestRequest($"{ApiPath}/{id}"));
    public virtual async Task<bool> Delete(List<int> tList) => await g.Client.DeleteAsync<bool>(tList.GetTRequest($"{ApiPath}/Bulk"));

    public virtual async Task<T?> Save(T t)
    {
        var e = await g.Client.PatchAsync<T>(t.GetTRequest(ApiPath));
        //e.CreateHash();
        return e;
    }

    public virtual async Task<List<T>?> Save(List<T> tList)
    {
        var eList = await g.Client.PatchAsync<List<T>>(tList.GetTRequest($"{ApiPath}/Bulk"));
        //foreach (var x in eList)
        //    x.CreateHash();
        return eList;
    }
}
