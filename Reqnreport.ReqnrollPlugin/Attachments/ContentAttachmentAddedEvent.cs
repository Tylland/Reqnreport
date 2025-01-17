using Reqnroll.Events;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments
{
    public class ContentAttachmentAddedEvent(string name, string content, string type) : ExecutionEvent
    {
        public string Name { get; set; } = name;
        public string Content { get; set; } = content;
        public string Type { get; set; } = type;
    }
}
