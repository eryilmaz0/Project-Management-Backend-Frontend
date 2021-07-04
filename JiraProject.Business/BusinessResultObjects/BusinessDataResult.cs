namespace JiraProject.Business.BusinessResultObjects
{
    public abstract class BusinessDataResult<T> : BusinessResult, IBusinessDataResult<T>
    {
        public T Data { get; set; }


        //User-Friendly Constructors
        public BusinessDataResult(T data,bool isSuccess):base(isSuccess)
        {
            this.Data = data;
        }



        public BusinessDataResult(T data,bool isSuccess, string message):base(isSuccess,message)
        {
            this.Data = data;
        }


        
    }
}