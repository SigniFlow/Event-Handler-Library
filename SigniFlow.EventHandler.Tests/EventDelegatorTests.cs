using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Moq;
using SigniFlow.EventHandler;
using SigniFlow.EventHandler.ConfigurationModels;
using SigniFlow.EventHandler.Exceptions;
using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler.Tests
{
    [TestFixture(Description = "Tests for EventDelegator")]
    [ExcludeFromCodeCoverage]
    public class EventDelegatorTests
    {
        private Mock<IEventHandler> _eventHandler;
        private EventDelegator _eventDelegator;

        [SetUp]
        public void Setup()
        {
            this.SetupEventHandlerMock();

            var authOptions = new EventHandlerAuthOptions("testSecret");

            var sfEvent = new SigniFlowEvent(sfs: "testSecret", et: "Document Added", di: "123", ui: "123",
                ue: "test@example.com", ed: DateTime.Now, "", "", adf:"additional data");
            this._eventDelegator =
                EventDelegatorFactory.GetEventDelegator(sfEvent, this._eventHandler.Object, authOptions);
        }

        private void SetupEventHandlerMock()
        {
            this._eventHandler = new Mock<IEventHandler>();
        }

        [Test(Description = "Checks that the constructor sets the event type property correctly")]
        public void Constructor_SetsEventTypeProperty()
        {
            Assert.NotNull(this._eventDelegator.EventType);
        }

        [Test(Description = "Checks that the constructor sets the event handler property correctly")]
        public void Constructor_SetsEventHandlerProperty()
        {
            Assert.NotNull(this._eventDelegator.EventHandler);
        }

        //Test cases for Calls_Correct_Event_Handler_Method
        private static IEnumerable<CallsCorrectEventHandlerParameter> EventHandlerMethodMockTestCases
        {
            get
            {
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentAdded(),
                    EventType = SigniFlowEventType.DocumentAdded
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentApproved(),
                    EventType = SigniFlowEventType.DocumentApproved
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentCancelled(),
                    EventType = SigniFlowEventType.DocumentCancelled
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentCompleted(),
                    EventType = SigniFlowEventType.DocumentCompleted
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentDeleted(),
                    EventType = SigniFlowEventType.DocumentDeleted
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentPendingRelease(),
                    EventType = SigniFlowEventType.DocumentPendingRelease
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentRejected(),
                    EventType = SigniFlowEventType.DocumentPlaceholderRejected
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentReleased(),
                    EventType = SigniFlowEventType.DocumentReleased
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleDocumentSigned(),
                    EventType = SigniFlowEventType.DocumentSigned
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandlePlaceholderAdded(),
                    EventType = SigniFlowEventType.DocumentPlaceholderAdded
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandlePlaceholderReplaced(),
                    EventType = SigniFlowEventType.DocumentPlaceholderReplaced
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandlePlaceholderReverted(),
                    EventType = SigniFlowEventType.DocumentPlaceholderReverted
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandlePlaceholderUploaded(),
                    EventType = SigniFlowEventType.DocumentPlaceholderUploaded
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleFormSubmitted(),
                    EventType = SigniFlowEventType.FormSubmitted
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleTemplateCreated(),
                    EventType = SigniFlowEventType.PrepperTemplateCreated
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleRemplateRemoved(),
                    EventType = SigniFlowEventType.PrepperTemplateRemoved
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleTemplateUpdated(),
                    EventType = SigniFlowEventType.PrepperTemplateUpdated
                };
                yield return new CallsCorrectEventHandlerParameter
                {
                    MethodToCheck = (IEventHandler eh) => eh.HandleUnknownEvent(),
                    EventType = SigniFlowEventType.Unknown
                };
            }
        }

        [Test(Description = "Checks that the event delegator calls the correct method on the given event handler")]
        [TestCaseSource(nameof(EventHandlerMethodMockTestCases))]
        public async Task Calls_Correct_Event_Handler_Method(CallsCorrectEventHandlerParameter parameters)
        {
            //Setup the method that is to be checked
            this._eventHandler.Setup(parameters.MethodToCheck);
            //Call the method
            this._eventDelegator.EventType = parameters.EventType;
            await this._eventDelegator.HandleEvent();

            //Verify that the method has been called
            this._eventHandler.Verify(parameters.MethodToCheck);
        }

        [Test(Description =
            "Checks that the event delegator throws an InvalidEventTypeException if the event type is invalid")]
        [TestCase(255)]
        public void Throws_InvalidEventTypeException_If_Event_Type_Invalid(SigniFlowEventType invalidEventType)
        {
            this._eventDelegator.EventType = invalidEventType;
            //Assert that the correct exception is thrown
            Assert.ThrowsAsync<InvalidEventTypeException>( async () => await this._eventDelegator.HandleEvent());
        }

        public class CallsCorrectEventHandlerParameter
        {
            public Expression<Action<IEventHandler>> MethodToCheck { get; set; }
            public SigniFlowEventType EventType { get; set; }
        }
    }
}
