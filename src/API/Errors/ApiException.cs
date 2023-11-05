namespace API.Errors;
public class ApiException : ApiResponse
{
    public string Details { get; set; }
    public ApiException(int statusCode, string message = "", string details = "")
        : base(statusCode, message)
    {
        Details = details;
    }
}
