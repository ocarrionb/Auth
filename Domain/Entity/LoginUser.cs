using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class LoginUser
    {
        public User? User { get; set; }

        public string Token { get; set; }
    }
}
