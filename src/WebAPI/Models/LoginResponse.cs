namespace WebAPI.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
