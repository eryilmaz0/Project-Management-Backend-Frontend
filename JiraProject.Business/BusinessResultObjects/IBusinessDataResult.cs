namespace JiraProject.Business.BusinessResultObjects
{
    public interface IBusinessDataResult<T> : IBusinessResult
    {
         T Data { get; set; }
    }
}