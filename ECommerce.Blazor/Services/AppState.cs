namespace ECommerce.Blazor.State
{
    public class AppState
    {
        public bool IsLoggedIn { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }

        public bool IsAdmin => Role == "Admin";
    }
}
