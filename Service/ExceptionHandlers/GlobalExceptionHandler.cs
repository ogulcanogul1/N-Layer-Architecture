﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace App.Service.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorAsDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError); // 500 hatası

            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            httpContext.Response.ContentType = "application/json"; 
            
            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken:cancellationToken);

            return true;
        }
    }
}
