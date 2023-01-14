using System.Collections.Generic;
using NAIHelper.Database.UI_Entities.BaseEntities;
using NAIHelper.ViewModels;

namespace NAIHelper.Utils.Extensions
{
    public static class TrackerExtension
    {
        public static T AsTracking<T>(this T obj) where T : TrackedEntity
        {
            obj.AcceptChanges();
            return obj;
        }

        public static List<T> StartTracking<T>(this List<T> tList) where T : TrackedEntity
        {
            foreach (var x in tList)
                x.StartTracking();

            return tList;
        }
    }
}
