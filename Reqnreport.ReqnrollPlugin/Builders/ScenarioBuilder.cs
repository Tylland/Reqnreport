using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class ScenarioBuilder(string title) : IBuilder<ScenarioRecord>
    {
        public string Title { get; set; } = title;
        public string Description { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public List<FileAttachment> Attachments { get; set; } = [];
        public List<ContentAttachment> Contents { get; set; } = [];

        public TestResult TestResult { get; set; } = TestResult.Unknown;

        public StepBuilder Steps { get; set; } = new StepBuilder();

        public ScenarioBuilder WithDescription(string description)
        {
            Description = description;

            return this;
        }
        public ScenarioBuilder WithTags(string[] tags)
        {
            Tags = tags;

            return this;
        }

        public ScenarioBuilder WithAttachment(string name, string path)
        {
            Attachments.Add(new FileAttachment(name, path));

            return this;
        }
        public ScenarioBuilder WithContent(string name, string content, string type)
        {
            Contents.Add(new ContentAttachment(name, content, type));

            return this;
        }

        public ScenarioBuilder WithTestResult(TestResult result)
        {
            TestResult = result;

            return this;
        }

        public ScenarioBuilder AddStep(string step)
        {
            Steps.AddStep(step);

            return this;
        }

        public ScenarioRecord Build()
        {
            var locations = Tags.Where(tag => RequirementLocationTag.Matches(tag)).Select(tag => RequirementLocationTag.Parse(tag));
            var numbers = Tags.Where(tag => RequirementNumberTag.Matches(tag)).Select(tag => RequirementNumberTag.Parse(tag));

            var references = new List<ScenarioReference>();

            foreach (var location in locations)
            {
                foreach (var number in numbers)
                {
                    references.Add(new ScenarioReference(number, location + "?anchor=" + number.ToLower()));
                }
            }

            return new ScenarioRecord(Title, Description, references, Contents, Steps.Build(), TestResult);
        }
    }
}
