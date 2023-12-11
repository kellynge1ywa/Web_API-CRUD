using System.Net;

namespace Todo;

public class ResponseDto
{
    public HttpStatusCode StatusCode {get;set;}
    public string? Message {get;set;}
    public object? Result {get;set;}

}
