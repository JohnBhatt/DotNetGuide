using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace DotNetGuide.Domain.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string? StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        public string? State { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }

        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool IsEnable { get; set; } = true;
    }
}
