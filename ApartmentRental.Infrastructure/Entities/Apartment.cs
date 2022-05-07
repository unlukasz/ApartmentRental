using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Entities
{
    public class Apartment:BaseEntity
    {
        public decimal RentAmount { get; set; }
        public int NumberOfRooms { get; set; }
        public int SquareMeters { get; set; }
        public int Floor { get; set; }
        public bool IsElevator { get; set; }

        public int LandLordId { get; set; }
        public LandLord LandLord { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
