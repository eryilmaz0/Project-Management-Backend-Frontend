namespace JiraProject.Business.BusinessResultObjects
{
    public class ErrorDataResult<T> : BusinessDataResult<T>
    {

        //User-Friendly Constructors
        public ErrorDataResult() : base(default, false)
        {

        }



        public ErrorDataResult(string message) : base(default, false, message)
        {

        }



        public ErrorDataResult(T data) : base(data, false)
        {

        }



        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
    }
}