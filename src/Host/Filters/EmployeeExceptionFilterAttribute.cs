using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Host.Filters;

public class EmployeeExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public EmployeeExceptionFilterAttribute()
    {
        // MissingFieldException
        // JsonException
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(CsvHelper.MissingFieldException), HandleBadRequestException },
                { typeof(JsonException), HandleBadRequestException },
                { typeof(EmployeeParseFromJsonException), HandleBadRequestException },
                { typeof(DuplicateKeyException), HandleBadRequestException },
            };
        
    }
    public override void OnException(ExceptionContext context)
    {
        Handle(context);
        base.OnException(context);
    }

    private void Handle(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }
    }

    private void HandleBadRequestException(ExceptionContext context)
    {
        // TODO: define ProblemDetails
        ProblemDetails details = new()
        {
            Title = context.Exception.Message,
            Detail = context.Exception.StackTrace,
        };
        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ValidationProblemDetails(exception.Errors);

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

}
