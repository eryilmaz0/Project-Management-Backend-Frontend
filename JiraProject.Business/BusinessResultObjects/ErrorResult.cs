namespace JiraProject.Business.BusinessResultObjects
{
    public class ErrorResult : BusinessResult
    {

        public ErrorResult():base(false)
        {
            
        }


        public ErrorResult(string message):base(false,message)
        {
            
        }
    }
}