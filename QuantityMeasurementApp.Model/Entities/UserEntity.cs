using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Model.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        // 🔴 MUST BE NULLABLE FOR GOOGLE LOGIN
        public string? PasswordHash { get; set; }

        public string? Salt { get; set; }

        public string Role { get; set; } = "User";
    }
}