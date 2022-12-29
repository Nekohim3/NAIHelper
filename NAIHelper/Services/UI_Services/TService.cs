using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.ViewModels.UI_Entities;

namespace NAIHelper.Services.UI_Services
{
    public abstract class TService<T> where T : UI_Entity, new()
    {
        protected virtual string GetAll()
        {

        }
    }
}
