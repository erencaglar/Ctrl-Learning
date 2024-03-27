using CtrlEdu.Data;
using CtrlEdu.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CtrlEdu.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
