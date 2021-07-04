namespace JiraProject.Business.BusinessResultObjects
{
    public class SuccessResult : BusinessResult
    {

        public SuccessResult():base(true)
        {
            
        }


        public SuccessResult(string message) : base(true, message)
        {

        }
    }
}