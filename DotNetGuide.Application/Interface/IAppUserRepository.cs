using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetGuide.Domain.Entities;

namespace DotNetGuide.Application.Interface
{
    public interface IAppUserRepository:IRepository<ApplicationUser>
    {
        /// <summary>
        /// This method will get the user name by Id and return it as a string. This is useful for displaying the user's name in the UI while database has value in Guid format.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetUserNameByIdAsync(Guid? userId);

        /// <summary>
        /// This method will get the user name by Id and return it as a string. This is useful for displaying the user's name in the UI while database has value in Guid format.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetUserNameById(Guid? userId);

        void Update(ApplicationUser obj);
    }
}
