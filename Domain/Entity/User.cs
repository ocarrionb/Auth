using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Role { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
