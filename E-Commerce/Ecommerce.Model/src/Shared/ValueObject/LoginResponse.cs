using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Model.src.Shared.ValueObject
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Is_Admin { get; set; } = false;
        public int Id { get; set; }
    }
}
