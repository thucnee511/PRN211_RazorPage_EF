using System;
using System.Collections.Generic;

namespace SE171089_BusinessObjects
{
    public partial class Rent
    {
        public Rent()
        {
            RentDetails = new HashSet<RentDetail>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? TotalQuatity { get; set; }
        public DateTime? RentTime { get; set; }
        public DateTime? ReturnTime { get; set; }
        public string? Status { get; set; }

        public virtual Account User { get; set; } = null!;
        public virtual ICollection<RentDetail> RentDetails { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, UserId: {UserId}, TotalQuatity: {TotalQuatity}, RentTime: {RentTime}, ReturnTime: {ReturnTime}, Status: {Status}";
        }
    }
}
