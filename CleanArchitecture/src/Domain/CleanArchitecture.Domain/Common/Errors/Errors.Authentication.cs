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
        public static class Authentication
        {
            public static Error InvalidCredential => 
                Error.Validation(code: "Auth.InvalidCredential", description: "Invalid Credential");
        }
    }
}
