using Reqnroll.Events;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Attachments;
using Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders;

namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin
{
    public class RequirementReportEventListner : IExecutionEventListener
    {
        public ScenarioBuilder? CurrentScenarioBuilder { get; set; }

        public void OnEvent(IExecutionEvent executionEvent)
        {
            if (executionEvent is NamedFileAttachmentAddedEvent evt)
            {
                CurrentScenarioBuilder?.WithAttachment(evt.Name, evt.FilePath);
            }

            if (executionEvent is ContentAttachmentAddedEvent contentEvent)
            {
                CurrentScenarioBuilder?.WithContent(contentEvent.Name, contentEvent.Content, contentEvent.Type);
            }
        }
    }
}
