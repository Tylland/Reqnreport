namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model
{
    public class ContentAttachment(string name, string content, string type)
    {
        public string Name { get; set; } = name;
        public string Content { get; set; } = content;
        public string Type { get; set; } = type;
    }
}
