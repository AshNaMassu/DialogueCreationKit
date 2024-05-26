namespace DialogueCreationKit.DialogueKit.Domain.Responses;

public class BaseResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public bool Status { get; set; }
}