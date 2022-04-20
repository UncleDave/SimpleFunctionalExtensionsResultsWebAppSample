using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResultsWebAppSample.Domain;
using SimpleFunctionalExtensions;

namespace ResultsWebAppSample.Infrastructure;

public class UnwrapResultAttribute : ActionFilterAttribute
{
    private static readonly Dictionary<MyErrorType, int> ErrorStatusCodeMap = new()
    {
        [MyErrorType.FailedToDoSomething] = StatusCodes.Status418ImATeapot,
        [MyErrorType.SomethingNotFound] = StatusCodes.Status404NotFound
    };
    
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult { Value: IErrorResult<MyError> result })
            return;

        if (result.IsSuccess)
        {
            if (result is IValueResult<object> valueResult)
            {
                context.Result = new OkObjectResult(valueResult.Value);
                return;
            }

            context.Result = new NoContentResult();
            return;
        }

        var statusCode = ErrorStatusCodeMap.ContainsKey(result.Error.Type)
            ? ErrorStatusCodeMap[result.Error.Type]
            : 500;

        context.Result = new ObjectResult(result.Error)
        {
            StatusCode = statusCode
        };
    }
}