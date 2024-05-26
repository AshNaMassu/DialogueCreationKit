namespace DialogueCreationKit.DialogueKit.Domain.Model.Responses;

public class BaseResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public bool Status { get; set; }
}