using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Database.UI_Entities;

namespace NAIHelper.Database.Services;

public class SessionService : TService<Session>
{
    public SessionService() : base("Sessions")
    {
    }
}
