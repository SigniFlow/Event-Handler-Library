namespace SigniFlow.EventHandler.ConfigurationModels
{
    public class EventHandlerAuthOptions
    {
        public EventHandlerAuthOptions(string signiFlowSecret) {
            SigniFlowSecret = signiFlowSecret;
        }
        public string SigniFlowSecret { get; set; }
    }
}
