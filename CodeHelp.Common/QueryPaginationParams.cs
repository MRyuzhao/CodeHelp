namespace CodeHelp.Common
{
    public class QueryPaginationParams
    {
        public int StartNo { get; set; }
        public int EndNo { get; set; }
        public string OrderByPropertyName { get; set; }
        public string IsAsc { get; set; }

        public QueryPaginationParams(int currentPage, int pageSize, string orderByPropertyName, bool isAsc)
        {
            StartNo = (currentPage - 1) * pageSize + 1;
            EndNo = pageSize * currentPage;
            OrderByPropertyName = string.IsNullOrEmpty(orderByPropertyName) ? "ColumnColid" : orderByPropertyName;
            IsAsc = isAsc ? "DESC" : "ASC";
        }
    }
}
