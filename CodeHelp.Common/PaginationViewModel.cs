using System.Collections.Generic;

namespace CodeHelp.Common
{
    public class PaginationViewModel<T>
    {
        public PaginationViewModel(IList<T> list, int currentPage, int pageSize, int total)
        {
            List = list;
            Pagination = new Pagination(pageSize, total, currentPage);
        }
        public IList<T> List { get; set; }

        public Pagination Pagination { get; set; }
    }
}