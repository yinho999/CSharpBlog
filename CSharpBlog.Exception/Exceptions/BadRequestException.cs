namespace CSharpBlog.Exception.Exceptions;

public class BadRequestException: ApplicationException
{
    public BadRequestException( string message): base(message)
    {
        
    }
}