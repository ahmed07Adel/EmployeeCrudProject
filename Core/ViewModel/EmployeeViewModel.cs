using Core.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mobile { get; set; }
        public int? EducationId { get; set; }
        [Required]
        [EmailAddress]
        [ValidEmailDomain(AllowDomain: "com", ErrorMessage = "Invalid Email, Add .com")]
        public string Email { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
