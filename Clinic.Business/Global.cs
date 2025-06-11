using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Clinic.Business
{
    internal static class Global
    {
        private static readonly string _loggingSource;
        static Global()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // or Directory.GetCurrentDirectory()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _loggingSource = configuration["AppSettings:LoggingSource"]
                ?? throw new InvalidOperationException("Logging Source setting not found");
        }

        public static bool CheckExistenceOfLoggingSource() =>
            EventLog.SourceExists(_loggingSource);

        public static void Log(string message,EventLogEntryType type)
        {
            if (!EventLog.SourceExists(_loggingSource))
                EventLog.CreateEventSource(_loggingSource, "Clinic");

            EventLog.WriteEntry(_loggingSource, message, type);
        }

    }
}
