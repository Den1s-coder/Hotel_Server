using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces.Repo;
using Hotel.Infrastracture.Data;

namespace Hotel.Infrastracture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelDBContext _context;

        public UserRepository(HotelDBContext context) 
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }
    }
}
