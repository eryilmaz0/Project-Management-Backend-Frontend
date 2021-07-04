namespace JiraProject.Business.BusinessResultObjects
{
    public interface IBusinessResult
    {
         bool IsSuccess { get; set; }
         string Message { get; set; }
    }
}