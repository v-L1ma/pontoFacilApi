using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
)
    {
        logger.LogError(exception, "Ocorreu uma exceção não tratada");

        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            NaoEncontradoException => StatusCodes.Status404NotFound,
            EmUsoException => StatusCodes.Status400BadRequest,
            ParametroInvalidoException => StatusCodes.Status400BadRequest, 
            _ => StatusCodes.Status500InternalServerError
        };

        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Ocorreu um erro, tente novamente",
                Detail = exception.Message
            }
        );
        return true;
    }
}
