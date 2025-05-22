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
_Microsoft.AspNetCore.Identity.EntityFrameworkCore - Version 8.0.16  
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
 
# Explanation of Features

## Using Brevo api for Email sending  
Add you API Key in appsettings.json and update EmailSender.cs inside DotNetGuide.Application.Utility to change name and other values. 

