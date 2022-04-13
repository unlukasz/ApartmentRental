using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Entities
{
    internal class Image:BaseEntity
    {
        public byte[] Data { get; set; }

        public int ApartamentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}
