﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetGuide.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public string? CompanyId { get; set; }
    }
}
