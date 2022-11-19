using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class User
        {
            public static Error DuplicateError => 
                Error.Conflict(code: "User.DuplicatedEmail", description: "Email already exist");
        }
    }
}
