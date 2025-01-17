
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Model;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class TestCaseBuilder 
    {
        private readonly Builders<FeatureBuilder, string, FeatureRecord> _features = new();

        public FeatureBuilder WithFeature(string title)
        {
            return _features.With(title, t => new FeatureBuilder(t));
        }
    }
}
