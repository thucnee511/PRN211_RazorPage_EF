using System;
using System.Collections.Generic;

namespace SE171089_BusinessObjects
{
    public partial class RentDetail
    {
        public int Id { get; set; }
        public int? RentId { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Rent? Rent { get; set; }
    }
}
