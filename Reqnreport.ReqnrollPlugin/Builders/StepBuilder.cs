namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class StepBuilder : IBuilder<string[]>
    {
        public List<string> Steps { get; set; } = [];

        public StepBuilder AddStep(string step)
        {
            Steps.Add(step);

            return this;
        }

        public string[] Build()
        {
            return [.. Steps];
        }
    }
}
