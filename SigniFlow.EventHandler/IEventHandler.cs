using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler
{
    public interface IEventHandler
    {
        SigniFlowEvent SigniflowEvent { get; set; }
        async Task<SigniFlowEventResult> HandleDocumentAdded()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentApproved()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentCancelled()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentDeleted()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentCompleted()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentPendingRelease()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentRejected()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentReleased()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleDocumentSigned()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandlePlaceholderAdded()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandlePlaceholderReplaced()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandlePlaceholderReverted()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandlePlaceholderUploaded()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleFormSubmitted()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleTemplateCreated()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleRemplateRemoved()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
        async Task<SigniFlowEventResult> HandleTemplateUpdated()
        {
            return SigniFlowEventResult.SuccessfulEvent;
        }
    }
}
