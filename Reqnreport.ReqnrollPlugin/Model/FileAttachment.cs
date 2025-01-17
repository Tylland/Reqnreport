namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model
{
    public class FileAttachment(string name, string filePath)
    {
        public string Name { get; set; } = name;
        public string FilePath { get; set; } = filePath;
    }
}
