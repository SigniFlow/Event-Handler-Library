using System.Diagnostics.CodeAnalysis;
using Moq;
using SigniFlow.EventHandler;
using SigniFlow.EventHandler.ConfigurationModels;
using SigniFlow.EventHandler.Exceptions;
using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class EventDelegatorFactoryTests
    {
        private SigniFlowEvent _signiflowEvent;
        private Mock<IEventHandler> _eventHandler;
        private EventHandlerAuthOptions _eventHandlerAuthOptions;
        [SetUp]
        public void Setup()
        {
            this._eventHandler = new Mock<IEventHandler>();

            this._eventHandlerAuthOptions = new EventHandlerAuthOptions("testSecret");

            this._signiflowEvent = new SigniFlowEvent(sfs: "testSecret", et: "Document Added", di: "123", ui: "123",
                ue: "test@example.com", ed: DateTime.Now, "", "");
        }

        [Test]
        public void GetEventDelegator_ReturnEventDelegator()
        {
            EventDelegatorFactory.GetEventDelegator(this._signiflowEvent, this._eventHandler.Object,
                this._eventHandlerAuthOptions);
        }

        [Test]
        [TestCase(null)]
        [TestCase("An invalid event type")]
        public void GetEventDelegator_ThrowsInvalidEventTypeException_OnNullOrInvalidValue(string value)
        {

            this._signiflowEvent.ET = value;
            Assert.That(
                () => EventDelegatorFactory.GetEventDelegator(this._signiflowEvent, this._eventHandler.Object,
                    this._eventHandlerAuthOptions),
                Throws.TypeOf<InvalidEventTypeException>()
                    .With.Message.EqualTo($"Err: Unknown event type - '{this._signiflowEvent.ET}'"));
        }

        [Test]
        public void GetEventDelegator_ThrowsInvalidSigniFlowSecretException_OnInvalidSecret()
        {
            this._signiflowEvent.SFS = "incorrect secret";
            Assert.That(
                () => EventDelegatorFactory.GetEventDelegator(this._signiflowEvent, this._eventHandler.Object,
                    this._eventHandlerAuthOptions),
                Throws.TypeOf<InvalidSigniFlowSecretException>()
                    .With.Message.EqualTo("Err: SigniFlow Secret does not match."));
        }

        [Test]
        public void GetEventDelegator_ReturnsEventDelegator()
        {
            //Act
            var eventDelegator = EventDelegatorFactory.GetEventDelegator(this._signiflowEvent,
                this._eventHandler.Object, this._eventHandlerAuthOptions);
            //Assert
            Assert.That(eventDelegator, Is.InstanceOf<EventDelegator>());
        }

        [Test]
        [TestCase("Document Added", SigniFlowEventType.DocumentAdded)]
        [TestCase("Document Approved", SigniFlowEventType.DocumentApproved)]
        [TestCase("Document Cancelled", SigniFlowEventType.DocumentCancelled)]
        [TestCase("Document Delete", SigniFlowEventType.DocumentDeleted)]
        [TestCase("Document Pending Release", SigniFlowEventType.DocumentPendingRelease)]
        [TestCase("Document Placeholder Added", SigniFlowEventType.DocumentPlaceholderAdded)]
        [TestCase("Document Placeholder Replaced", SigniFlowEventType.DocumentPlaceholderReplaced)]
        [TestCase("Document Placeholder Reverted", SigniFlowEventType.DocumentPlaceholderReverted)]
        [TestCase("Document Placeholder Uploaded", SigniFlowEventType.DocumentPlaceholderUploaded)]
        [TestCase("Document Rejected", SigniFlowEventType.DocumentPlaceholderRejected)]
        [TestCase("Document Released", SigniFlowEventType.DocumentReleased)]
        [TestCase("Document Signed", SigniFlowEventType.DocumentSigned)]
        [TestCase("Form Submitted", SigniFlowEventType.FormSubmitted)]
        [TestCase("Prepper Template Created", SigniFlowEventType.PrepperTemplateCreated)]
        [TestCase("Prepper Template Removed", SigniFlowEventType.PrepperTemplateRemoved)]
        [TestCase("Prepper Template Updated", SigniFlowEventType.PrepperTemplateUpdated)]
        public void GetEventDelegator_EventTypeGetsSetToCorrectValue(string givenEventType,
            SigniFlowEventType expectedEventType)
        {
            //Arrange
            this._signiflowEvent.ET = givenEventType;
            //Act
            var eventDelegator = EventDelegatorFactory.GetEventDelegator(this._signiflowEvent,
                this._eventHandler.Object, this._eventHandlerAuthOptions);
            //Assert
            Assert.That(eventDelegator.EventType, Is.EqualTo(expectedEventType));
        }

        [Test(Description = ("Tests that the event handler's signflow event property gets set"))]
        public void EventHandler_SigniflowEvent_Property_Gets_Set()
        {
            //Arrange
            this._eventHandler.SetupProperty(p => p.SigniflowEvent);
            //Act
            var eventDelegator = EventDelegatorFactory.GetEventDelegator(this._signiflowEvent,
                this._eventHandler.Object, this._eventHandlerAuthOptions);

            //Assert
            Assert.That(eventDelegator.EventHandler.SigniflowEvent, Is.Not.Null);
        }
    }
}
