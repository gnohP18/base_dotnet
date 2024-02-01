using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace base_dotnet.Common
{
    public static class Enum
    {
        public enum UserRole
        {
            [Display(Name = "Admin")]
            Admin = 1,
            [Display(Name = "User")]
            User = 2
        }

        public enum AccountStatus
        {
            Active,
            InActive
        }

    }
}