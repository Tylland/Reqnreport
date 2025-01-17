using System.Text;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin
{
    public class RequirementReportDocument(string path)
    {
        private readonly string _path = path ?? throw new ArgumentNullException(nameof(path));
        private readonly Builders<FeatureBuilder, string, FeatureRecord> _features = [];

        public FeatureBuilder WithFeature(string title)
        {
            return _features.With(title, t => new FeatureBuilder(t));
        }

        public void Save()
        {
            foreach (var feature in _features.Build())
            {
                Save(feature);
            }
        }

        private int CompareScenarions(ScenarioRecord scenario1, ScenarioRecord scenario2)
        {
            var requirement1 = scenario1.RequirementReferences.Select(rr => rr.Label).FirstOrDefault(string.Empty);
            var requirement2 = scenario2.RequirementReferences.Select(rr => rr.Label).FirstOrDefault(string.Empty);

            return string.Compare(requirement1, requirement2, StringComparison.Ordinal);
        }

        private void Save(FeatureRecord feature)
        {
            var md = new StringBuilder();

            md.AppendLine($"# {feature.Title}");
            md.AppendLine($" {feature.Description}");

            md.AppendLine(string.Empty);
            md.AppendLine($"## Scenarier");

            var scenarios = feature.Scenarios.ToList();

            scenarios.Sort(CompareScenarions);

            foreach (var scenario in scenarios)
            {
                var verified = scenario.Verified ? "x" : " ";

                md.AppendLine($"- [{verified}] {scenario.Title}");
            }

            md.AppendLine(string.Empty);


            foreach (var scenario in scenarios) 
            {
                var verified = scenario.Verified ? "Verifierad" : "Inte verifierad";

                md.AppendLine(string.Empty);
                md.AppendLine($"## {scenario.Title} - Status: {verified}");

                if (!string.IsNullOrEmpty(scenario.Description))
                {
                    md.AppendLine($" _{scenario.Description}_");
                }

                if (scenario.RequirementReferences.Any())
                {
                    foreach (var reqRef in scenario.RequirementReferences)
                    {
                        md.AppendLine($">[{reqRef.Label}]({reqRef.Path})");
                    }
                }

                md.AppendLine("``` Gherkin");

                foreach (var step in scenario.Steps)
                {
                    md.AppendLine(step);
                }

                md.AppendLine("```");


                if (scenario.Attachments.Any())
                {
                    md.AppendLine("<details>");
                    md.AppendLine("<summary>Output</summary>");

                    foreach (var attachment in scenario.Attachments)
                    {
                        md.AppendLine("<details>");
                        md.AppendLine($"<summary>{attachment.Name}</summary>");
                        md.AppendLine("<p>");
                        md.AppendLine(string.Empty);

                        md.AppendLine($"``` {attachment.Type}");
                        md.AppendLine($"{attachment.Content}");
                        md.AppendLine("```");
                        md.AppendLine(string.Empty);

                        md.AppendLine("</p>");
                        md.AppendLine("</details>");
                    }

                    md.AppendLine("</details>");
                    md.AppendLine(string.Empty);
                }
            }

            md.AppendLine(string.Empty);
            md.AppendLine($"_Genererad: {DateTime.Now}_");

            File.WriteAllText(_path, md.ToString());
        }
    }
}
