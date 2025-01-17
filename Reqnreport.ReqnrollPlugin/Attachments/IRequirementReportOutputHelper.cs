using Microsoft.Extensions.Logging;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments
{
    public interface IRequirementReportOutputHelper
    {
        void AddAttachment(string name, string type, byte[] content, string fileExtension = "");
        void AddContent(string name, string content, string type);
        void WriteLogLine(string message);
        void LogLine(LogLevel logLevel, string message);
        void AddLog(string name);
    }
}