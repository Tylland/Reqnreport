using System.Text;
using Microsoft.Extensions.Logging;
using Reqnroll.Events;
using Reqnroll.Infrastructure;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments
{
    public class RequirementReportOutputHelper(
        ITestThreadExecutionEventPublisher testThreadExecutionEventPublisher,
        IReqnrollAttachmentHandler reqnrollAttachmentHandler)
        : IRequirementReportOutputHelper
    {
        private static readonly string[] Levels = ["TRC", "DBG", "INF", "WRN", "ERR", "CRT"];

        private readonly StringBuilder _log = new();

        public void AddAttachment(string name, string type, byte[] content, string fileExtension = "")
        {
            var directory = Path.Combine(Environment.CurrentDirectory, new RequirementReportSettings().DirectoryName);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = Path.Combine(directory, Guid.NewGuid().ToString() + "_attachment.json");

            File.WriteAllBytes(filePath, content);

            testThreadExecutionEventPublisher.PublishEvent(new NamedFileAttachmentAddedEvent(name, filePath));
            reqnrollAttachmentHandler.AddAttachment(filePath);
        }

        public void AddContent(string name, string content, string type)
        {
            testThreadExecutionEventPublisher.PublishEvent(new ContentAttachmentAddedEvent(name, content, type));
        }

        public void AddLog(string name)
        {
            testThreadExecutionEventPublisher.PublishEvent(new ContentAttachmentAddedEvent(name, _log.ToString(), "LOG"));

            _log.Clear();
        }

        public void WriteLogLine(string message)
        {
            _log.AppendLine(message);
        }
        public void LogLine(LogLevel logLevel, string message)
        {

        _log.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {Levels[(int)logLevel]} - {message}");
        }
    }
}
