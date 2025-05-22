using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DotNetGuide.Application.Interface;
using DotNetGuide.Domain.Entities;
using DotNetGuide.Infrastructure.Data;

namespace DotNetGuide.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;
      
        public IAppUserRepository ApplicationUsers { get; private set; }
      
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ApplicationUsers = new AppUserRepository(_context);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<SelectList> GetUserSelectListAsync(Guid? selectedValue = null)
        {
            var users = await _context.ApplicationUsers.ToListAsync();
            return new SelectList(users, "Id", "Name", selectedValue);
        }

    }
}
