using System.Threading.Tasks;
using JiraProject.Business.Exceptions;
using JiraProject.Business.ValidationRules;
using Microsoft.AspNetCore.Http;

namespace JiraProject.Entities.Extensions
{
    public static class ExceptionHandlerMiddlewareExtensions
    {

        public static Task ValidationErrorResponse(this HttpResponse httpResponse, ValidationException exception)
        {
            httpResponse.StatusCode = 400;
            httpResponse.ContentType = "application/json";

            return httpResponse.WriteAsync(exception.ValidationError.ToString());
        }



        public static Task InternalServerErrorResponse(this HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 500;
            httpResponse.ContentType = "application/json";

            return httpResponse.WriteAsync(new ResponseErrorDetails("Sunucu Kaynaklı Bir Hata Oluştu.", 500).ToString());
        }
    }
}