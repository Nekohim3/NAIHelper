using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.Utils;
using NAIHelper.Utils.Extensions;
using RestSharp;

namespace NAIHelper.Database.Services
{
    public class SetupService
    {
        private readonly string ApiPath = "Setup";

        public virtual async Task<(bool success, string msg)> SetupContext()
        {
            var res = await g.ApiClient.ExecuteAsync(new RestRequest($"{ApiPath}/{g.Settings.DatabaseHost}/{g.Settings.DatabasePort}/{g.Settings.DatabaseName}/{g.Settings.DatabaseUsername}/{g.Settings.DatabasePassword}"));
            if (res.StatusCode == 0)
            {
                return (false, $"Cannot connect to api server (wrong addres / port?)\n{string.Join("\n", res.ErrorException.FromChain(_ => _.InnerException).Select(_ => _.Message))}");
            }
            else if (res.ResponseStatus == ResponseStatus.Aborted)
            {
                throw new Exception($"SetupService > SetupContext > ResponseStatus.Aborted\n{string.Join("\n", res.ErrorException.FromChain(_ => _.InnerException).Select(_ => _.Message))}");
            }
            else if (res.ResponseStatus == ResponseStatus.None)
            {
                throw new Exception($"SetupService > SetupContext > ResponseStatus.None\n{string.Join("\n", res.ErrorException.FromChain(_ => _.InnerException).Select(_ => _.Message))}");
            }
            else
            {
                if(res.IsSuccessStatusCode)
                    return (true, "");
                else
                {
                    if (res.StatusCode == HttpStatusCode.BadRequest)
                        return (false, $"Error ({res.StatusCode})\n{res.Content}");
                    else
                        throw new Exception($"SetupService > SetupContext > ResponseStatus.Aborted\n{string.Join("\n", res.ErrorException.FromChain(_ => _.InnerException).Select(_ => _.Message))}");
                }
            }
        }
    }
}
