using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http;

namespace WebApiDemo.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private static readonly string LogFilePath = Path.Combine(AppContext.BaseDirectory, "custom-exceptions.log");

    public void OnException(ExceptionContext context)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Timestamp: {DateTime.UtcNow:o}");
        builder.AppendLine($"Path: {context.HttpContext.Request.Path}");
        builder.AppendLine($"Message: {context.Exception.Message}");
        builder.AppendLine($"StackTrace: {context.Exception.StackTrace}");
        builder.AppendLine(new string('-', 80));

        File.AppendAllText(LogFilePath, builder.ToString());

        context.Result = new ExceptionResult(context.Exception, true);

        context.ExceptionHandled = true;
    }
}