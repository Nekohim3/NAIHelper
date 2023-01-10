using System;
using System.Collections.Generic;
using System.Linq;
using NAIHelper.Utils;
using Newtonsoft.Json;

namespace NAIHelper.ViewModels.UI_Entities.BaseEntities;

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
                           };
    }

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
        {
            _baseValues[x.Name] = x.GetValue(this);
        }

        IsChanged = false;
        return;
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
