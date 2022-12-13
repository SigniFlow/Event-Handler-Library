using SigniFlow.EventHandler.Exceptions;
using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler
{
    public enum SigniFlowEventType
    {
        /* 
        * Event Type:   Document Added
        * Description:  Document has been uploaded to the system.
        */
        DocumentAdded,
        /* 
        * Event Type:   Document Approved
        * Description:  Document has been approved by a user in the workflow who had an approve action instead of sign.
        */
        DocumentApproved,
        /* 
        * Event Type:   Document Cancelled
        * Description:  Document workflow has been cancelled.
        */
        DocumentCancelled,
        /* 
        * Event Type:   Document Completed
        * Description:  Document workflow has been completed.
        */
        DocumentCompleted,
        /* 
        * Event Type:   Document Delete
        * Description:  Document has been marked as deleted.
        */
        DocumentDeleted,
        /* 
        * Event Type:   Document Pending Release
        * Description:  Document workflow creation has taken place but document not yet released for workflow.
        */
        DocumentPendingRelease,
        /* 
        * Event Type:   Document Placeholder Added
        * Description:  A document placeholder has been added to a portfolio.
        */
        DocumentPlaceholderAdded,

        /* 
        * Event Type:   Document Placeholder Replaced
        * Description:  A document in the placeholder has been replaced with another document
        */
        DocumentPlaceholderReplaced,
        /* 
        * Event Type:   Document Placeholder Reverted
        * Description:  A document in the placeholder has been reverted back to an empty placeholder.
        */
        DocumentPlaceholderReverted,
        /* 
        * Event Type:   Document Placeholder Uploaded
        * Description:  A document has been uploaded into a placeholder.
        */
        DocumentPlaceholderUploaded,
        /* 
        * Event Type:   Document Rejected
        * Description:  Document workflow has been rejected.
        */
        DocumentPlaceholderRejected,
        /* 
        * Event Type:   Document Released
        * Description:  Document has been released for workflow.
        */
        DocumentReleased,
        /* 
        * Event Type:   Document Signed
        * Description:  Document has been signed by a user in the workflow.
        */
        DocumentSigned,
        /* 
        * Event Type:   Form Submitted
        * Description:  A form has been uploaded.
        */
        FormSubmitted,
        /* 
        * Event Type:   Prepper Template Created
        * Description:  A document prepper template has been created on the system.
        */
        PrepperTemplateCreated,
        /* 
        * Event Type:   Prepper Template Removed
        * Description:  A document prepper template has been removed from the system.
        */
        PrepperTemplateRemoved,
        /* 
        * Event Type:   Prepper Template Updated
        * Description:  A document prepper template has been updated on the system.
        */
        PrepperTemplateUpdated
    }
    
    /// <exception cref="InvalidEventTypeException">If the event type is unknown</exception>
    public class EventDelegator
    {
        public IEventHandler EventHandler { get; set; }
        public SigniFlowEventType EventType { get; set; }

        public EventDelegator(IEventHandler eventHandler, SigniFlowEventType eventType)
        {
            this.EventHandler = eventHandler;
            this.EventType = eventType;
        }

        public async Task<SigniFlowEventResult> HandleEvent()
        {
            return this.EventType switch
            {
                SigniFlowEventType.DocumentAdded => await this.EventHandler.HandleDocumentAdded(),
                SigniFlowEventType.DocumentApproved => await this.EventHandler.HandleDocumentApproved(),
                SigniFlowEventType.DocumentCancelled => await this.EventHandler.HandleDocumentCancelled(),
                SigniFlowEventType.DocumentCompleted => await this.EventHandler.HandleDocumentCompleted(),
                SigniFlowEventType.DocumentDeleted => await this.EventHandler.HandleDocumentDeleted(),
                SigniFlowEventType.DocumentPendingRelease => await this.EventHandler.HandleDocumentPendingRelease(),
                SigniFlowEventType.DocumentPlaceholderAdded => await this.EventHandler.HandlePlaceholderAdded(),
                SigniFlowEventType.DocumentPlaceholderReplaced => await this.EventHandler.HandlePlaceholderReplaced(),
                SigniFlowEventType.DocumentPlaceholderReverted => await this.EventHandler.HandlePlaceholderReverted(),
                SigniFlowEventType.DocumentPlaceholderUploaded => await this.EventHandler.HandlePlaceholderUploaded(),
                SigniFlowEventType.DocumentPlaceholderRejected => await this.EventHandler.HandleDocumentRejected(),
                SigniFlowEventType.DocumentReleased => await this.EventHandler.HandleDocumentReleased(),
                SigniFlowEventType.DocumentSigned => await this.EventHandler.HandleDocumentSigned(),
                SigniFlowEventType.FormSubmitted => await this.EventHandler.HandleFormSubmitted(),
                SigniFlowEventType.PrepperTemplateCreated => await this.EventHandler.HandleTemplateCreated(),
                SigniFlowEventType.PrepperTemplateRemoved => await this.EventHandler.HandleRemplateRemoved(),
                SigniFlowEventType.PrepperTemplateUpdated => await this.EventHandler.HandleTemplateUpdated(),
                _ => throw new InvalidEventTypeException($"Err: Unknown event type - '{this.EventType}'")
            };
        }
    }
}
