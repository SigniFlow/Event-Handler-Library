namespace SigniFlow.EventHandler.HttpModels;
/// <summary>
/// Result of an Event Handler processing an event
/// </summary>
public class SigniFlowEventResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public SigniFlowEventResult(bool success, string message)
    {
        this.Success = success;
        this.Message = message;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{(this.Success ? "Suc" : "Err")}:{this.Message}";
    }

    /// <returns>A <see cref="SigniFlowEventResult"/> with <see cref="Success"/> true and a <see cref="Message"/> of "EventHandled".</returns>
    public static SigniFlowEventResult SuccessfulEvent => new SigniFlowEventResult(true, "EventHandled");
    /// <returns>A <see cref="SigniFlowEventResult"/> with <see cref="Success"/> false and a <see cref="Message"/> of "EventFailed".</returns>
    public static SigniFlowEventResult FailedEvent => new SigniFlowEventResult(false, "EventFailed");
}
