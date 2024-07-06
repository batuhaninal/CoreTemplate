using API.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IO;
using System.Diagnostics;
using System.Reflection;

namespace API.Middlewares
{
    public class RequestResponseActivityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _memoryStreamManager;

        public RequestResponseActivityMiddleware(RequestDelegate next)
        {
            _next = next;
            _memoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var actionDescriptor = context.GetEndpoint()?
                .Metadata
                .GetMetadata<ControllerActionDescriptor>();

            bool? b = actionDescriptor?.MethodInfo.CustomAttributes.Any( x=> x.AttributeType == typeof(SecureOperationAttribute));

            if (b.HasValue && !b.Value)
            {
                await AddRequestBodyContentToActivityTagAsync(context);
            }
            await AddResponseBodyContentToActivityTagAsync(context);
        }

        private async Task AddRequestBodyContentToActivityTagAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var requestBodyStreamReader = new StreamReader(context.Request.Body);

            var requestBodyContent = await requestBodyStreamReader.ReadToEndAsync();

            Activity.Current?.SetTag("http.request.body", requestBodyContent);

            context.Request.Body.Position = 0;
        }

        private async Task AddResponseBodyContentToActivityTagAsync(HttpContext context)
        {
            var originalResponseBody = context.Response.Body;

            await using var responseBodyMemoryStream = _memoryStreamManager.GetStream();

            context.Response.Body = responseBodyMemoryStream;

            await _next(context);

            responseBodyMemoryStream.Position = 0;

            var responseBodyContent = await new StreamReader(responseBodyMemoryStream).ReadToEndAsync();

            Activity.Current?.SetTag("http.response.body", responseBodyContent);

            responseBodyMemoryStream.Position = 0;

            await responseBodyMemoryStream.CopyToAsync(originalResponseBody);
        }
    }
}
