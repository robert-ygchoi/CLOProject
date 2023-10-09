namespace Application.Common.Exceptions;

public class EmployeeParseFromJsonException : Exception
{
    public string Content { get; set; }

    public EmployeeParseFromJsonException(string content)
    {
        Content = content;
    }

}
