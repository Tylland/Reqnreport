using Reqnroll;
using Reqnroll.Events;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin
{
    public static class ReqnrollExtensions
    {
        public static string StepText(this IScenarioStepContext stepContext)
        {
            return stepContext.StepInfo.StepInstance.Keyword + stepContext.StepInfo.StepInstance.Text;
        }
        public static string FeatureTitle(this HookFinishedEvent evt)
        {
            return evt?.FeatureContext?.FeatureInfo?.Title ?? string.Empty;
        }
        public static string ScenarioTitle(this HookFinishedEvent evt)
        {
            return evt?.ScenarioContext?.ScenarioInfo?.Title ?? string.Empty;
        }


    }
}
