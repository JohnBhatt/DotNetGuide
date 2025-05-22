using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetGuide.Application.Interface
{
    public interface IUnitOfWork
    {
       
        IAppUserRepository ApplicationUsers { get; }
        void Save();

        Task<int> SaveAsync();
        /// <summary>
        /// This method will get the list of users and return their Id and Full Name for using inside Dropdown or SelectList 
        /// </summary>
        /// <param name="selectedValue">In case you are performing Edit operation, you can pass the selectedValue</param>
        /// <returns></returns>
        Task<SelectList> GetUserSelectListAsync(Guid? selectedValue = null);     
    }
}
