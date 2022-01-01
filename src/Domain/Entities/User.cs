using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
