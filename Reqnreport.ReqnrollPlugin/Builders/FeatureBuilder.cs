using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class FeatureBuilder(string title) : IBuilder<FeatureRecord>
    {
        public string Title { get; set; } = title;
        public string? Description { get; set; }

        private readonly Builders<ScenarioBuilder, string, ScenarioRecord> _scenarios = [];

        public FeatureBuilder WithDescription(string description)
        {
            Description = description;

            return this;
        }

        public ScenarioBuilder WithScenario(string title)
        {
            return _scenarios.With(title, t => new ScenarioBuilder(t));
        }

        public FeatureRecord Build()
        {
            return new FeatureRecord(Title, Description, _scenarios.Build());
        }
    }
}
