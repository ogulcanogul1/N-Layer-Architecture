using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.ExceptionHandlers
{
    internal class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException) 
            {
                Console.WriteLine("Sms Gönderildi");
                // işlemler ... 
            }
            return ValueTask.FromResult(false);
        }
    }
}
