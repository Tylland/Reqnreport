using Reqnroll.Events;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments
{
    public class NamedFileAttachmentAddedEvent(string name, string filePath) : AttachmentAddedEvent(filePath)
    {
        public string Name { get; set; } = name;
    }
}
