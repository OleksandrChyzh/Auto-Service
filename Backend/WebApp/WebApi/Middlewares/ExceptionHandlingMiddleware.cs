using System.Text.Json;
using BLL.DTOs;
using BusinessLogic.Models;
using DAL.Exceptions;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = GenerateErrorResponse(exception);

        context.Response.StatusCode = errorResponse.StatusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private static ErrorResponse GenerateErrorResponse(Exception exception)
    {
        return exception switch
        {
            ManageIdentityException miEx => new ErrorResponse(miEx.Message, StatusCodes.Status400BadRequest),
            UnauthorizedAccessException uaEx => new ErrorResponse(uaEx.Message, StatusCodes.Status403Forbidden),
            NotFoundException nfEx => new ErrorResponse(nfEx.Message, StatusCodes.Status404NotFound),
            _ => new ErrorResponse("An unexpected error occurred.", StatusCodes.Status500InternalServerError),
        };
    }
}
