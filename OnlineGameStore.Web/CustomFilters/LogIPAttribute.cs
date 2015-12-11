using System;
using System.Diagnostics;
using System.Web;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace OnlineGameStore.CustomFilters
{
    /// <summary>
    /// Parallel filter for logging user IP address
    /// </summary>
    /// <param name="actionContext">contains information about action and request</param>
    /// <param name="cancellationToken">token for cancelling operation</param>
    /// <param name="continuation">executing action; filter is applied for this action</param>
    /// <returns>message with ip addresse</returns>
    public class LogIPAttribute : Attribute, IActionFilter
    {
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            HttpResponseMessage response = await continuation();
            var userIP = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "undefined";
            response.Headers.Add("User-IP", userIP);

            Log.Debug("User IP address is: {userIp}", userIP);
            return response;
        }

        /// <summary>
        /// prohibits calling of multiple instances of filter to one action
        /// </summary>
        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}