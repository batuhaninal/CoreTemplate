using Application.Models.DTOs.Commons.Results;
using Application.Utilities.Exceptions.Commons;
using FluentValidation;
using FluentValidation.Results;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException bs)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResultDto(httpContext.Response.StatusCode, bs.Message)));
                return;
            }
            catch (ValidationException ve)
            {
                httpContext.Response.ContentType = "application/json";

                string message = ve.Message;
                IEnumerable<ValidationFailure> errors = ((ValidationException)ve).Errors;
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Success = false,
                    Message = message,
                    Errors = errors
                }));
                return;
            }
            catch (Exception e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                string message = "Internal Server Error";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResultDto(httpContext.Response.StatusCode, message)));
                return;
            }
        }
    }
}
