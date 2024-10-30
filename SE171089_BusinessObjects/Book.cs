using System;
using System.Collections.Generic;

namespace SE171089_BusinessObjects
{
    public partial class Book
    {
        public Book()
        {
            RentDetails = new HashSet<RentDetail>();
        }

        public int Id { get; set; }
        public int CateId { get; set; }
        public string Name { get; set; } = null!;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }

        public virtual Category Cate { get; set; } = null!;
        public virtual ICollection<RentDetail> RentDetails { get; set; }
    }
}
