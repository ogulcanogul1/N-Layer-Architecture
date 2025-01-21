using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Service
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore] public bool IsError => !IsSuccess;
        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore] public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ServiceResult<T> result = new ServiceResult<T> { Data = data , StatusCode = statusCode  };
            return result;
        }

        public static  ServiceResult<T> SuccessAsCreated(T data , string url)
        {
            return new ServiceResult<T>() { Data = data , StatusCode=HttpStatusCode.Created , UrlAsCreated = url };
        }

        public static ServiceResult<T> Fail(List<string> errorMessage , HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ServiceResult<T> result = new ServiceResult<T> { ErrorMessage = errorMessage , StatusCode = statusCode };
            return result;
        }

        public static ServiceResult<T> Fail(string errorMessage , HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ServiceResult<T> result = new ServiceResult<T> { ErrorMessage = new List<string> { errorMessage } , StatusCode = statusCode };
            return result;
        }

    }


    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore] public bool IsError => !IsSuccess;
        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            ServiceResult result = new ServiceResult { StatusCode = statusCode };
            return result;
        }

        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ServiceResult result = new ServiceResult { ErrorMessage = errorMessage, StatusCode = statusCode };
            return result;
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ServiceResult result = new ServiceResult { ErrorMessage = new List<string> { errorMessage }, StatusCode = statusCode };
            return result;
        }

    }
}
