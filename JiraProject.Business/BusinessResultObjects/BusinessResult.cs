namespace JiraProject.Business.BusinessResultObjects
{
    public abstract class BusinessResult : IBusinessResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }


       
        public BusinessResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }


        public BusinessResult(bool isSuccess, string message):this(isSuccess)
        {
            this.Message = message;
        }
    }
}