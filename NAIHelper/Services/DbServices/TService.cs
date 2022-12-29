using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NAIHelper.Database;
using NAIHelper.Models;
using ReactiveUI;

namespace NAIHelper.Services.DbServices
{
    public class TService<T> where T : IdEntity, new()
    {
        /// <summary>
        /// Возвращает все записи данного типа
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> GetRange()
        {
            using var ctx = new TagContext();
            return ctx.Set<T>().AsEnumerable().ToList();
        }

        /// <summary>
        /// Добавляет несуществующие записи в бд и сохраняет существующие записи в бд
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        protected virtual bool SaveRange(List<T> tList)
        {
            using var ctx  = new TagContext();
            var       news = tList.Where(_ => _.Id == 0);
            var       olds = tList.Where(_ => _.Id != 0);
            ctx.AddRange(news);
            ctx.UpdateRange(olds);
            return ctx.SaveChanges() > 0;
        }

        /// <summary>
        /// Удаляет существующие записи в бд
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        protected virtual bool DeleteRange(List<T> tList)
        {
            using var ctx  = new TagContext();
            var       existed = tList.Where(_ => _.Id != 0);
            foreach (var x in existed)
                ctx.Entry(x).State = EntityState.Deleted;
            return ctx.SaveChanges() > 0;
        }
    }
}
