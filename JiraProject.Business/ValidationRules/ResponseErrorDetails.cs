using Newtonsoft.Json;

namespace JiraProject.Business.ValidationRules
{
    public class ResponseErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }



        public ResponseErrorDetails(string message, int statusCode)
        {
            this.Message = message;
            this.StatusCode = statusCode;
        }
    }
}