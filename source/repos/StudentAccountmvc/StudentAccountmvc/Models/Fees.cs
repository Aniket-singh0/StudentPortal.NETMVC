
using System.ComponentModel.DataAnnotations;

namespace StudentAccountmvc.Models
{
    public class Fees
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        public string FeeType { get; set; } = "";

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal PendingAmount
        {
            get
            {
                return TotalAmount - PaidAmount;
            }
        }

        public string Status
        {
            get
            {
                if (PaidAmount >= TotalAmount)
                    return "Paid";

                if (PaidAmount > 0)
                    return "Partial";

                return "Pending";
            }
        }
    }
}

