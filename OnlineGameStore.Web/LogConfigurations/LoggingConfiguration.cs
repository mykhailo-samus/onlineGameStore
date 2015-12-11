using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Events;
using System.IO;

namespace OnlineGameStore.Web.Logs
{
    public static class LoggingConfiguration
    {

        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Logs\Log-{Date}.txt"))
                         .WriteTo.EventLog("GameStore")
                         .CreateLogger();
        }
    }
}