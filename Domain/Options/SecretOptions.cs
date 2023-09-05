using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Options
{
    public class SecretOptions
    {
        public const string Secrets = "Secrets";
        public string SecretKey { get; set; } = String.Empty;
    }
}
