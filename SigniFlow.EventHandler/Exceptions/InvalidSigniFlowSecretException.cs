namespace SigniFlow.EventHandler.Exceptions
{

    [Serializable]
    public class InvalidSigniFlowSecretException : Exception
    {
        public InvalidSigniFlowSecretException() { }
        public InvalidSigniFlowSecretException(string message) : base(message) { }
        public InvalidSigniFlowSecretException(string message, Exception inner) : base(message, inner) { }
        protected InvalidSigniFlowSecretException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
