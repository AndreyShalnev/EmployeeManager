namespace GorestClient.Data
{
    public class PagedResponse<TData>
        where TData : class
    {
        public int Code { get; set; }
        public Meta Meta { get; set; }
        public TData Data { get; set; }
    }
}
