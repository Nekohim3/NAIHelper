using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using NAIHelper.ViewModels;
using NAIHelper.ViewModels.UI_Entities;
using NAIHelper.ViewModels.UI_Entities.BaseEntities;
using Newtonsoft.Json;

namespace NAIHelper.Utils;

public static class TManager
{
    public static void CopyTo<T>(this T source, T destination) where T : class, new()
    {
        foreach (var x in typeof(T).GetProperties()
                                   .Where(p => p.CanWrite && ((!p.PropertyType.IsClass && !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)) ||
                                                              p.PropertyType == typeof(string))))
        {
            var ps = x.GetValue(source,      null);
            var pd = x.GetValue(destination, null);
            if (!IsEqual(ps, pd))
                x.SetValue(destination, ps, null);
        }
    }

    public static T GetCopy<T>(this T source) where T : class, new()
    {
        var destination = new T();
        foreach (var x in typeof(T).GetProperties()
                                   .Where(p => p.CanWrite && ((!p.PropertyType.IsClass && !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)) ||
                                                              p.PropertyType == typeof(string))))
        {
            var ps = x.GetValue(source,      null);
            var pd = x.GetValue(destination, null);
            if (!IsEqual(ps, pd))
                x.SetValue(destination, ps, null);
        }

        return destination;
    }

    //public static int GetHash<T>(this T source) where T : class, new()
    //{
    //    var hash = new HashCode();
    //    var type = source.GetType();
    //    while (type != typeof(Entity))
    //    {
    //        foreach (var p in type.GetProperties())
    //        {
    //            if (p.CanWrite && ((!p.PropertyType.IsClass && !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)) || p.PropertyType == typeof(string)))
    //                hash.Add(p.GetValue(source, null));
    //        }
    //        if (type.BaseType == null) break;
    //        type = type.BaseType;
    //    }

    //    return hash.ToHashCode();
    //}

    public static byte[] GetHash<T>(this T source) where T : class
    {
        var objList = new List<object?>();
        var type    = source.GetType();
        while (type != typeof(Entity))
        {
            foreach (var p in type.GetProperties())
            {
                if (p.CanWrite && ((!p.PropertyType.IsClass && !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)) || p.PropertyType == typeof(string)))
                    objList.Add(p.GetValue(source, null));
            }

            if (type.BaseType == null)
                break;
            type = type.BaseType;
        }

        return MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objList)));
    }

    public static List<PropertyInfo> GetProperties(this object source) => source.GetType().GetProperties()
                                                                                .Where(p => p.CanWrite && ((!p.PropertyType.IsClass && !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)) || p.PropertyType == typeof(string))).ToList();

    private static bool IsEqual(object? val1, object? val2)
    {
        if (val1 == null && val2 == null)
            return true;
        if (val1 == null || val2 == null)
            return false;
        return val1.Equals(val2);
    }

    private static bool IsEqual(string val1, string val2)
    {
        if (string.IsNullOrWhiteSpace(val1) && string.IsNullOrWhiteSpace(val2))
            return true;
        if (string.IsNullOrWhiteSpace(val1) && !string.IsNullOrWhiteSpace(val2))
            return false;
        return val1.Equals(val2);
    }
}
