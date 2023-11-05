namespace API.Errors;
public class ApiValidationErrorResponse : ApiResponse
{
    public List<string> Errors { get; set; } = new();
    public ApiValidationErrorResponse()
        : base(400)
    {

    }
}
