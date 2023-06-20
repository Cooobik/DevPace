namespace DevPace.Domain.Models.CustomerModels
{
    public class CustomerFindNSortModel
    {
        public string? SearchItem { get; set; }
        public string? OrderBy { get; set; }
        public bool OrderDescending { get; set; }
        public int RowCount { get; set; }
        public int PageNumber { get; set; }
    }
}
