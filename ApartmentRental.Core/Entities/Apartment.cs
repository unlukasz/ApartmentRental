using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Core.Entities
{
    internal class Apartment:BaseEntity
    {
        public decimal RentAmount { get; set; }
        public int NumberofRooms { get; set; }
        public int SquereMeters { get; set; }
        public int Floor { get; set; }
        public bool IsElevator { get; set; }

        public int LandlordId { get; set; }
        public Landlord Landlord { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
