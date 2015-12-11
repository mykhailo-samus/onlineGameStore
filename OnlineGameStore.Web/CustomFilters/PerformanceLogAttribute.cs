using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

public class PerformanceLogAttribute: Attribute, IActionFilter
{
    /// <summary>
    /// Parallel filter for logging performance
    /// </summary>
    /// <param name="actionContext">contains information about action and request</param>
    /// <param name="cancellationToken">token for cancelling operation</param>
    /// <param name="continuation">executing action; filter is applied for this action</param>
    /// <returns>message with measured data about performance</returns>
    public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    {
            Stopwatch timer = Stopwatch.StartNew();
            HttpResponseMessage response = await continuation();
            var actionName = actionContext.ActionDescriptor.ActionName;

            double seconds = timer.ElapsedMilliseconds / 1000.0;
            response.Headers.Add("Action-Executed", String.Format("{0} ticks", timer.ElapsedTicks.ToString()));

            Log.Debug(@"Action : {actionName} executed for - ""{seconds}"" sec", actionName, seconds);
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