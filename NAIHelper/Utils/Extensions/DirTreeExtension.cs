using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExCSS;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
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
        
        public static IEnumerable<TSource> FromChain<TSource>(
            this TSource?           source,
            Func<TSource, TSource?> nextItemSelector)
            where TSource : class
        {
            for (var current = source; current != null; current = nextItemSelector(current))
                yield return current;
        }

        public static IEnumerable<TSource> FromTree<TSource>(
            this TSource?                          source,
            Func<TSource, IEnumerable<TSource?>?> nextItemSelector)
            where TSource : class
        {
            if (source == null)
                yield break;
            yield return source;
            if (nextItemSelector(source) == null)
                yield break;
            foreach (var x in nextItemSelector(source))
                foreach (var c in FromTree(x, nextItemSelector))
                    yield return c;
        }
    }
}
