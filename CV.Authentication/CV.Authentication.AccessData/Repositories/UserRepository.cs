using CV.Authentication.AccessData.Interfaces;
using CV.Authentication.Domain.Mappers;
using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV.Authentication.AccessData.Repositories
{
    public  class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }        
        public async Task AddUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public List<User> GetAllUsers() => _context.User.Include(e => e.UserAccountState).ToList();
        
        public async Task Update(User user)
        {
            _context.ChangeTracker.Clear();
            _context.Entry(user).State = EntityState.Detached;
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<UserDto?> GetUserToVerify(Guid id, string token)
        {
            var user = await _context.User.Where(e => e.Id == id).FirstOrDefaultAsync(e => e.VerificationToken == token);
            return user?.Map();
        }
        public async Task<UserDto?> GetUserToResetPassword(Guid id, string token)
        {
            var user = await _context.User.Where(e => e.Id == id).FirstOrDefaultAsync(e => e.PasswordResetToken == token);
            return user?.Map();
        }
        public async Task<UserDto?> GetUserById(Guid id) {
            var user = await _context.User.FindAsync(id);
            return user == null ? null : user.Map();
        }
        public async Task<bool> FindByEmailAsync(string email)
        {
            if (await _context.User.Where(u => u.Email == email).FirstOrDefaultAsync() != null)
                return true;
            else
                return false;
        }
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _context.User.Where(u => u.Email == email).Select(x => x.Map()).FirstOrDefaultAsync();
            if (user != null)
                return user;
            else
                return null;
        }
        public async Task<UserResponse> GetState(string email)
        {
            var user = await _context.User.Include(u => u.UserAccountState).Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
                return null;
            return new UserResponse { 
                Id = user.Id,
                Email = user.Email,
                State = new StateDto 
                {
                    StateId = user.UserAccountState.Id,
                    Description = user.UserAccountState.Description
                } };
        }
    }
}