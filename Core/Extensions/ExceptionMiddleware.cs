using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException e)
            {
                await HandleExceptionAsync(httpContext, e, (int)HttpStatusCode.Unauthorized);
            }
            catch (BadHttpRequestException e)
            {
                await HandleExceptionAsync(httpContext, e, (int)HttpStatusCode.BadRequest);
            }
            catch (ValidationException e)
            {
                await HandleExceptionAsync(httpContext, e, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        //async yoktu daha sonradan ekledim. (S)
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            object message = "Internal Server Error";

            //Şayet oluşan hata Bizim belirlediğimiz validasyon kontrolünden geliyorsa oradan gelen hatayı kullanıcıya dönder.

            if (e.GetType() == typeof(ValidationException))
                message = (e as ValidationException)?.Errors.Select(s => s.ErrorMessage);
            //else message=e.Message kısmı silinecek. Sadece görmek için eklendi.
            //else
            //message = e.Message;


            //MVC
            //httpContext.Response.Headers.Add("errorMessage", e.Message);
            //httpContext.Response.Headers.Keys.FirstOrDefault("errorMessage");
            //httpContext.Response.Headers.TryGetValue("errorMessage", out var messagedeneme);
            httpContext.Response.Redirect("/Error/"+statusCode);

            //API
            //return httpContext.Response.WriteAsJsonAsync(new ErrorDetails
            //{
            //    StatusCode = httpContext.Response.StatusCode,
            //    Message = message,
            //    Success = false
            //});
        }
    }
}
