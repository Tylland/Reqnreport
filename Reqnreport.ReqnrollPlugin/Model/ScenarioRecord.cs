namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model
{
    public record ScenarioRecord(string Title, string Description, IEnumerable<ScenarioReference> RequirementReferences, IEnumerable<ContentAttachment> Attachments, string[] Steps, TestResult TestResult)
    {
        public bool Verified => TestResult == TestResult.Passed;
    };
}
