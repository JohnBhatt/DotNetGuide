using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetGuide.Application.Utility;
using DotNetGuide.Domain.Entities;
using DotNetGuide.Infrastructure.Data;

namespace DotNetGuide.Infrastructure.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDb;
        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDb)
        {
            _appDb = appDb;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_appDb.Database.GetPendingMigrations().Count() > 0)
                {
                    _appDb.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }


            //Create Roles if they are already not present
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                //Create First Admin User as well, if Roles table is empty.

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "john@johnbhatt.com",
                    Email = "john@johnbhatt.com",
                    FullName = "John Bhatt",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Paschim Vihar",
                    State = "Delhi",
                    City = "New Delhi",
                    PostalCode = "110063",
                }, "John@123").GetAwaiter().GetResult();
                ApplicationUser user = _appDb.ApplicationUsers.FirstOrDefault(u => u.Email == "john@johnbhatt.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
