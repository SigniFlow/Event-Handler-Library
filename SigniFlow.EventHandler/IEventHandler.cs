using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler
{
    public interface IEventHandler
    {
        SigniFlowEvent SigniflowEvent { get; set; }
        async Task<SigniFlowEventResult> HandleDocumentAdded()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentApproved()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentCancelled()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentDeleted()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentCompleted()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentPendingRelease()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentRejected()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentReleased()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleDocumentSigned()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandlePlaceholderAdded()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandlePlaceholderReplaced()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandlePlaceholderReverted()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandlePlaceholderUploaded()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleFormSubmitted()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleTemplateCreated()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleRemplateRemoved()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleTemplateUpdated()
        {
            return await Task.FromResult(SigniFlowEventResult.SuccessfulEvent);
        }
        async Task<SigniFlowEventResult> HandleUnknownEvent()
        {
            return await Task.FromResult(new SigniFlowEventResult(true, "Unknown event"));
        }
    }
}
