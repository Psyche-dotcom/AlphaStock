namespace AlpaStock.Core.DTOs.Request.Blog
{
    public class RetrieveAllBlogFilter
    {
        public int pageNumber { get; set; }
        public int perPageSize { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string? UserId { get; set; }
        public string? sinceDate { get; set; }
        public string? Search { get; set; }
    }
}
