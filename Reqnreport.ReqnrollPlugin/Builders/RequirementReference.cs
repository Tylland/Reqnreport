namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class RequirementReference(string label, string url)
    {
        public string Label { get; set; } = label;
        public string Url { get; set; } = url;
    }
}
