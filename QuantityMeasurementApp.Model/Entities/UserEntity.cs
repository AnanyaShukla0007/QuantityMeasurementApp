using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Model.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }
    }
}