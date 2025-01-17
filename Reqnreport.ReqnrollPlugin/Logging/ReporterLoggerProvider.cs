using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Reqnroll;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Logging
{
    public class ReporterLoggerProvider(IReqnrollOutputHelper outputHelper, IRequirementReportOutputHelper reportHelper)
        : ILoggerProvider
    {
        private const string DefaultLoggerName = "DefaultLogger";

        private readonly ConcurrentDictionary<string, ILogger> _loggers = new();

        private readonly IExternalScopeProvider _externalScopeProvider = new LoggerExternalScopeProvider();
        private readonly IReqnrollOutputHelper _outputHelper = outputHelper ?? throw new ArgumentNullException(nameof(outputHelper));
        private readonly IRequirementReportOutputHelper _reportHelper = reportHelper ?? throw new ArgumentNullException(nameof(reportHelper));

        public ILogger CreateLogger(string categoryName)
        {
            var logger = _loggers.GetOrAdd(categoryName, name => new ReporterLogger(_externalScopeProvider, _outputHelper, _reportHelper));
            return logger;
        }

        public ILogger<T> CreateLogger<T>()
        {
            var loggerName = typeof(T).FullName ?? DefaultLoggerName;

            var logger = _loggers.GetOrAdd(loggerName, _ => new ReporterLogger<T>(_externalScopeProvider, _outputHelper, _reportHelper));

            return (ILogger<T>)logger;
        }

        public void Dispose()
        {
            _loggers?.Clear();
        }
    }
}
