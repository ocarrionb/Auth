using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "The UserName is required")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        public required string Password { get; set; }
    }
}
