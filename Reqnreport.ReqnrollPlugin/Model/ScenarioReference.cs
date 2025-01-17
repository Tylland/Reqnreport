namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model
{
    public class ScenarioReference(string label, string path)
    {
        public string Label { get; set; } = label;
        public string Path { get; set; } = path;
    }
}
