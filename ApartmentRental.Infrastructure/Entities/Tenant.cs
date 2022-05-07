using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Entities
{
    public class Tenant:BaseEntity
    {
        public Apartment Apartment { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
