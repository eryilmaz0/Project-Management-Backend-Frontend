using System.Collections.Generic;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class PagedResult<T>
    {
        public ICollection<T> Result { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ResultCount { get; set; }


        public bool HasNext
        {
            get => ResultCount > PageSize * PageNumber;
        }


        public bool HasPrevious
        {
            get => PageNumber > 1;

        }
    }
}