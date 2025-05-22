# DotNetGuide - ASP.NET Core 8.0 C# - Documentation & Explanation


## DotNetGuide.Application - Class Library
**Project Dependency:** _DotNetGuide.Domain_

**Nuget Packages:**   
_Microsoft.AspNetCore.Identity.EntityFrameworkCore - Version 8.0.16  
Microsoft.AspNetCore.Identity.UI - Version 8.0.16  
brevo_csharp - Version 1.1.1 - For Email sending purpose_


## DotNetGuide.Domain
**Project Dependency:** _none_  

**Nuget Packages:** 
_Microsoft.AspNetCore.Identity.EntityFrameworkCore - Version 8.0.16  
Microsoft.EntityFrameworkCore.Tools - Version 8.0.16 - To add migration and Database Update_

## DotNetGuide.Infrastructure
**Project Dependency:** _DotNetGuide.Application_  

**Nuget Packages:** 
_Microsoft.EntityFrameworkCore - Version 8.0.16  
Microsoft.AspNetCore.Identity.EntityFrameworkCore - Version 8.0.16  
Microsoft.EntityFrameworkCore.SqlServer - Version 8.0.16 - as default Database server. You can choose different based on your need.  
Microsoft.EntityFrameworkCore.Tools - Version 8.0.16 - To add migration and Database Update  
Microsoft.EntityFrameworkCore.Design - Version 8.0.16 - Dependency of Tools_  

## DotNetGuide.Web
**Project Dependency:**  
_DotNetGuide.Application  
DotNetGuide.Infrastructure_

**Nuget Packages:**  
_Microsoft.AspNetCore.Identity.EntityFrameworkCore - Version 8.0.16  
Microsoft.AspNetCore.Identity.UI - Version 8.0.16  
Microsoft.EntityFrameworkCore.SqlServer - Version 8.0.16 - as default Database server. You can choose different based on your need.  
Microsoft.EntityFrameworkCore.Tools - Version 8.0.16 - To add migration and Database Update  
Microsoft.EntityFrameworkCore.Design - Version 8.0.16 - Dependency of Tools_  

**Scaffolding Identity**  
Identity login pages generated using Right click on DotNetGuide.Web project> Add> New Scaffoled Item > Choose Identity from left sidebar > Choose the files > Choose required files (I have included all) > SelectDbContext class > Click on Add.
![image](https://github.com/user-attachments/assets/aaba43da-63eb-49f4-8763-967d156d88fa)


 
# Explanation of Features

## Using Brevo api for Email sending  
Add you API Key in appsettings.json and update EmailSender.cs inside DotNetGuide.Application.Utility to change name and other values. 

![image](https://github.com/user-attachments/assets/faf89bfd-8a0c-4bea-baea-c92ed9ab7200)

To add new Models.

1. Add Entity class inside Domain.Entities
2. Create a New Interface and Repository respectively inside Application and Infrastructure projects. You can create a copy of IAppUserRepository and AppUserRepository respectively, then rename them and update you entity.
3. Once Repository and Interfaces are created, register both of them inside IUnitOfWork and UnitOfWork, you can look at the existing one for AppUser.
4. Create a Controller and instead of injecting DbContext directly, inject Repository and UnitOfWork and create in views accordingly.  


