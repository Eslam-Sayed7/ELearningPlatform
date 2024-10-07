using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos
{
    public class PaymentRequestDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
    }

}
