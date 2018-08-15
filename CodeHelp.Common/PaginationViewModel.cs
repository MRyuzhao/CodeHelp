using System.Collections.Generic;

namespace CodeHelp.Common
{
    public class PaginationViewModel<T>
    {
        public PaginationViewModel(IList<T> list)
        {
            List = list;
        }
        public IList<T> List { get; set; }

        public string Pagination { get; set; }
    }
}