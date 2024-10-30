using System;
using System.Collections.Generic;

namespace SE171089_BusinessObjects
{
    public partial class Account
    {
        public Account()
        {
            Rents = new HashSet<Rent>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public int? Status { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
