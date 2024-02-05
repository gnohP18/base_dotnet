namespace base_dotnet.DTOs
{
    public class UserFilterOption
    {
        public string? KeySearch { get; set; }
        public int? UserTypeId { get; set; }
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }
}