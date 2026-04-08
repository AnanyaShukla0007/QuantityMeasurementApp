using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Repository.Data;

namespace QuantityMeasurementApp.Repository.Services
{
    public class UserRepository
    {
        private readonly QuantityDbContext _context;

        public UserRepository(QuantityDbContext context)
        {
            _context = context;
        }

        public void Add(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UserEntity? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}