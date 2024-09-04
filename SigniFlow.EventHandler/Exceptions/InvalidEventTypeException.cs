namespace SigniFlow.EventHandler.Exceptions;

[Serializable]
public class InvalidEventTypeException : Exception
{
    public InvalidEventTypeException() { }
    public InvalidEventTypeException(string message) : base(message) { }
    public InvalidEventTypeException(string message, Exception inner) : base(message, inner) { }
    protected InvalidEventTypeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}