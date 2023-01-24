using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NAIHelper.Utils;
using NAIHelper.ViewModels;
using Newtonsoft.Json;

namespace NAIHelper.Database.UI_Entities.BaseEntities;

public abstract class TrackedEntity : ViewModelBase
{
    private             Dictionary<string, object?> _baseValues = new();
    [JsonIgnore] public bool?                       IsChanged { get; set; }

    protected TrackedEntity()
    {
        PropertyChanged += (sender, args) =>
                           {
                               if (!IsChanged.HasValue)
                                   return;
                               if (string.IsNullOrEmpty(args.PropertyName) || sender == null)
                                   return;
                               if (sender.GetProperties().Count(_ => Attribute.IsDefined(_, typeof(TrackInclude)) && _.Name == args.PropertyName) != 0)
                               {
                                   var newValue = sender.GetType().GetProperty(args.PropertyName).GetValue(sender);
                                   if (_baseValues[args.PropertyName] == newValue)
                                   {
                                       IsChanged = false;
                                       foreach (var x in sender.GetProperties().Where(_ => Attribute.IsDefined(_, typeof(TrackInclude))))
                                       {
                                           if (x.Name == args.PropertyName)
                                               continue;
                                           if (_baseValues[x.Name] == null)
                                           {
                                               if (x.GetValue(sender) != null)
                                                   IsChanged = true;
                                           }
                                           else
                                           {
                                               if (_baseValues[x.Name].Equals(x.GetValue(sender)))
                                                   IsChanged = true;
                                           }
                                       }
                                   }
                                   else
                                   {
                                       IsChanged = true;
                                   }
                               }
                           };
    }

    public bool IsPropertyChanged(string propertyName) => _baseValues.ContainsKey(propertyName)
                                                              ? _baseValues[propertyName] != GetType().GetProperty(propertyName).GetValue(this)
                                                              : throw new Exception("Property is not tracked.");

    public void StartTracking()
    {
        IsChanged = false;
        AcceptChanges();
    }

    public void AcceptChanges()
    {
        if (!IsChanged.HasValue)
            throw new InvalidOperationException("Entity is not tracked.");
        _baseValues.Clear();
        foreach (var x in this.GetProperties().Where(_ => Attribute.IsDefined(_, typeof(TrackInclude))))
            _baseValues[x.Name] = x.GetValue(this);

        IsChanged = false;
    }

    public void RevertChanges()
    {
        if (!IsChanged.HasValue)
            throw new InvalidOperationException("Entity is not tracked.");
        foreach (var x in this.GetProperties().Where(_ => Attribute.IsDefined(_, typeof(TrackInclude))))
            x.SetValue(this, _baseValues[x.Name]);
        IsChanged = false;
    }
}

public static class TrackerExtension
{
    public static List<T> StartTracking<T>(this List<T> tList) where T : TrackedEntity
    {
        foreach (var x in tList)
            x.StartTracking();

        return tList;
    }

    public static List<T> AcceptChanges<T>(this List<T> tList) where T : TrackedEntity
    {
        foreach (var x in tList)
            x.AcceptChanges();

        return tList;
    }

    public static List<T> RevertChanges<T>(this List<T> tList) where T : TrackedEntity
    {
        foreach (var x in tList)
            x.RevertChanges();

        return tList;
    }



    public static bool IsPropertyChanged<TEntity, TProperty>(this TEntity model, Expression<Func<TEntity, TProperty>> propertySelector) where TEntity : TrackedEntity =>
        model.IsPropertyChanged((propertySelector.Body as MemberExpression).Member.Name);
}
