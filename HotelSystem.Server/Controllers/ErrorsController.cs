using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


/// <summary>
/// Контроллер для обработки ошибок приложения.
/// </summary>
[ApiController]
public class ErrorsController(ILogger<ErrorsController> logger) : ControllerBase
{
    /// <summary>
    /// Обрабатывает необработанное исключение
    /// </summary>
    [Route("/error")]
    public IActionResult HandleError()
    {
        var exceptionFeature = HttpContext.Features
            .Get<IExceptionHandlerPathFeature>();

        if (exceptionFeature?.Error is not null)
        {
            logger.LogError(
                exceptionFeature.Error,
                "Необработанное исключение при обработке запроса {Path}",
                exceptionFeature.Path);
        }

        return Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Внутренняя ошибка сервера",
            detail: "При обработке запроса произошла ошибка.");
    }
}