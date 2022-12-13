using SigniFlow.EventHandler.ConfigurationModels;
using SigniFlow.EventHandler.Exceptions;
using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler
{
    public static class EventDelegatorFactory
    {

        public static EventDelegator GetEventDelegator(SigniFlowEvent signiFlowEvent, IEventHandler eventHandler, EventHandlerAuthOptions eventHandlerAuthOptions)
        {

            if (!HasValidAuth(signiFlowEvent, eventHandlerAuthOptions))
            {
                throw new InvalidSigniFlowSecretException("Err: SigniFlow Secret does not match.");
            }

            var eventType = ConvertToSigniFlowEventType(signiFlowEvent.ET);
            eventHandler.SigniflowEvent = signiFlowEvent;
            return new EventDelegator(eventHandler, eventType);
        }

        private static bool HasValidAuth(SigniFlowEvent signiFlowEvent, EventHandlerAuthOptions eventHandlerAuthOptions)
        {
            return signiFlowEvent.SFS == eventHandlerAuthOptions.SigniFlowSecret;
        }

        /// <exception cref="InvalidEventTypeException">If the event type is unknown</exception>
        private static SigniFlowEventType ConvertToSigniFlowEventType(string eventType)
        {
            var typeToTest = eventType ?? "";
            return typeToTest.ToLower() switch
            {
                "document added" => SigniFlowEventType.DocumentAdded,
                "document approved" => SigniFlowEventType.DocumentApproved,
                "document cancelled" => SigniFlowEventType.DocumentCancelled,
                "document delete" => SigniFlowEventType.DocumentDeleted,
                "document pending release" => SigniFlowEventType.DocumentPendingRelease,
                "document rejected" => SigniFlowEventType.DocumentPlaceholderRejected,
                "document released" => SigniFlowEventType.DocumentReleased,
                "document signed" => SigniFlowEventType.DocumentSigned,
                "document completed" => SigniFlowEventType.DocumentCompleted,
                "document placeholder added" => SigniFlowEventType.DocumentPlaceholderAdded,
                "document placeholder replaced" => SigniFlowEventType.DocumentPlaceholderReplaced,
                "document placeholder reverted" => SigniFlowEventType.DocumentPlaceholderReverted,
                "document placeholder uploaded" => SigniFlowEventType.DocumentPlaceholderUploaded,
                "form submitted" => SigniFlowEventType.FormSubmitted,
                "prepper template created" => SigniFlowEventType.PrepperTemplateCreated,
                "prepper template removed" => SigniFlowEventType.PrepperTemplateRemoved,
                "prepper template updated" => SigniFlowEventType.PrepperTemplateUpdated,
                _ => throw new InvalidEventTypeException($"Err: Unknown event type - '{eventType}'")
            };
        }
    }
}
