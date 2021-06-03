using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicaretDB.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Parola { get; set; }
    }
}
