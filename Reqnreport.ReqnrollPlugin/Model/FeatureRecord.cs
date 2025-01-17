namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model
{
    public record FeatureRecord(string Title, string? Description, IEnumerable<ScenarioRecord> Scenarios);
}
