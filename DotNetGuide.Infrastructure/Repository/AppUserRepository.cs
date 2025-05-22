using Microsoft.EntityFrameworkCore;

using DotNetGuide.Application.Interface;
using DotNetGuide.Domain.Entities;
using DotNetGuide.Infrastructure.Data;

namespace DotNetGuide.Infrastructure.Repository
{
    public class AppUserRepository : Repository<ApplicationUser>, IAppUserRepository
    {
        public AppDbContext _context;
        public AppUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<string> GetUserNameByIdAsync(Guid? userId)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId.ToString());
            return user?.FullName ?? "Unknown";
        }

        public string GetUserNameById(Guid? userId)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId.ToString());
            return user?.FullName ?? "Unknown";
        }

        public void Update(ApplicationUser obj)
        {
            _context.ApplicationUsers.Update(obj);
        }
    }
}
