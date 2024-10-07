using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? TransactionDate { get; set; }

    }

}
