using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.DTOs
{
    public class ChangePasswordDto
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
