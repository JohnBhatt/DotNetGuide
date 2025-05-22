
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DotNetGuide.Domain.Entities;
using DotNetGuide.Application.Utility;
using DotNetGuide.Infrastructure.Data;
using DotNetGuide.Web.ViewModels;

namespace DotNetGuide.Controllers
{

    //Area protected for Admin Role users.
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userID)
        {
            RoleManagementVM roleVM = new RoleManagementVM() { 
            ApplicationUser = _context.ApplicationUsers.FirstOrDefault(u=>u.Id == userID), 
                RoleList = _roleManager.Roles.Select(i=> new SelectListItem { 
            Text=i.Name,
            Value=i.Name,
            }),
            CompanyList = _context.Companies.Select(i=>new SelectListItem
            {
                Text=i.Name,
                Value=i.Id.ToString(),
            }),
            };
            roleVM.ApplicationUser.Role = _userManager.GetRolesAsync(_context.ApplicationUsers.FirstOrDefault(u => u.Id == userID)).GetAwaiter().GetResult().FirstOrDefault();

            return View(roleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            string oldRole = _userManager.GetRolesAsync(_context.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id)).GetAwaiter().GetResult().FirstOrDefault();

            ApplicationUser applicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagementVM.ApplicationUser.Id);

            if (!(roleManagementVM.ApplicationUser.Role == oldRole))
            {
                if (roleManagementVM.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.Id = roleManagementVM.ApplicationUser.CompanyId;
                }
                if (oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }
                _context.ApplicationUsers.Update(applicationUser);
                _context.SaveChanges();
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult(); ;
            }
            else
            { 
            if(oldRole == SD.Role_Company && applicationUser.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                    _context.ApplicationUsers.Update(applicationUser);
                    _context.SaveChanges();
                }
            }
            TempData["success"] = "User Profile updated successfully!!";
            return RedirectToAction("Index");
        }



        #region API Calls
        [HttpGet]
//        public IActionResult GetAll()
//        {
//            List<ApplicationUser> objUserList = _unitofWork.ApplicationUser.GetAll().ToList();
////            var userRoles = _unitofWork.ApplicationUser.i 

//            return Json(new { data = objUserList });
//        }


        public IActionResult GetAll()
        {
            var objUserList = _context.ApplicationUsers.ToList();

            var userList = objUserList.Select(u =>
            {
                return new UserIndexVM
                {
                    Id = u.Id,
                    Name = u.FullName,
                    Email = u.Email,
                    City = u.City,
                    State = u.State,
                    PostalCode = u.PostalCode,
                    Role = _userManager.GetRolesAsync(u).GetAwaiter().GetResult().FirstOrDefault(),
                    CompanyName = string.IsNullOrEmpty(u.CompanyId) ? _context.Companies.Where(i => i.Id.ToString() == u.CompanyId).Select(i => i.Name).FirstOrDefault() ?? "N/A" : "N/A",
                    LockoutEnd = u.LockoutEnd
                };
            }).ToList();

            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnLock([FromBody]string id)
        {
            var userFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if(userFromDb== null)
            {
                return Json(new {success=false, message= "Error while locking/unlocking"});
            }

            if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
            {
                userFromDb.LockoutEnd = DateTime.Now;
            }
            else
            { 
            userFromDb.LockoutEnd = DateTime.Now.AddDays(30);
            }
            _context.ApplicationUsers.Update(userFromDb);
            _context.SaveChanges();
            return Json(new {success = true, message="Lock/Unlock Successfull."});
        }

        #endregion
    }
}
