﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Core.Entities
{
    internal class Landlord : BaseEntity
    {
        public List<Apartment> Apartments { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
