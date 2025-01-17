using Microsoft.Extensions.Logging;
using Reqnroll;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Logging
{
    public class ReporterLogger(
        IExternalScopeProvider externalScopeProvider,
        IReqnrollOutputHelper outputHelper,
        IRequirementReportOutputHelper reportHelper)
        : ILogger
    {
        private static readonly string[] Levels = ["TRC", "DBG", "INF", "WRN", "ERR", "CRT"];

        private readonly IExternalScopeProvider _externalScopeProvider = externalScopeProvider ?? throw new ArgumentNullException(nameof(externalScopeProvider));
        private readonly IReqnrollOutputHelper _outputHelper = outputHelper ?? throw new ArgumentNullException(nameof(outputHelper));
        private readonly IRequirementReportOutputHelper _reportHelper = reportHelper ?? throw new ArgumentNullException(nameof(reportHelper));

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return _externalScopeProvider.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _outputHelper.WriteLine(formatter(state, exception));
            _reportHelper.WriteLogLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {Levels[(int)logLevel]} - {formatter(state, exception)}");
        }
    }

    public class ReporterLogger<T>(
        IExternalScopeProvider externalScopeProvider,
        IReqnrollOutputHelper outputHelper,
        IRequirementReportOutputHelper reportHelper)
        : ReporterLogger(externalScopeProvider, outputHelper, reportHelper), ILogger<T>;
}
