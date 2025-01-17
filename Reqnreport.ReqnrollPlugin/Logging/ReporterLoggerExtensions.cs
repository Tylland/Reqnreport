using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Logging
{
    public static class ReporterLoggerExtensions
    {
        public static ILoggingBuilder AddReporterLogger(this ILoggingBuilder builder)
        {
            //            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ReporterLoggerProvider>());

            builder.Services.AddSingleton<ILoggerProvider, ReporterLoggerProvider>();

            return builder;
        }
    }
}
