using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExCSS;
using NAIHelper.Database.UI_Entities;

namespace NAIHelper.Utils.Extensions
{
    public static class DirTreeExtension
    {
        public static int GetAllNodesCount(this List<Dir> dirs)
        {
            var counter = 0;
            foreach (var x in dirs)
                GetChildsCount(x, ref counter);

            return counter;
        }

        private static void GetChildsCount(Dir dir, ref int counter)
        {
            counter++;
            foreach (var x in dir.Dirs)
                GetChildsCount(x, ref counter);
        }

        private static IEnumerable<TSource> FromChain<TSource>(
            this TSource?           source,
            Func<TSource, TSource?> nextItemSelector,
            Func<TSource?, bool>    canContinue)
        {
            for (var current = source; canContinue(current); current = nextItemSelector(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromChain<TSource>(
            this TSource?           source,
            Func<TSource, TSource?> nextItemSelector)
            where TSource : class
        {
            return FromChain(source, nextItemSelector, _ => _ != null);
        }
    }
}
