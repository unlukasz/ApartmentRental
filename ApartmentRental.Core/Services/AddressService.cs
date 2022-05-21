using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly MainContext _context;
        private readonly IAddressRepository _addressRepository;
        public AddressService(MainContext context, IAddressRepository addressRepository)
        {
            _context = context;
            _addressRepository = addressRepository;
        }
        public async Task<int> GetAddressIdOrCreateAsync(string country, string city, string zipCode,
            string street, string buildingNumber, string apartmentNumber)
        {
            var id = await _addressRepository.GetAddressIdByItsAttributesAsync(country, city, zipCode, street, buildingNumber, apartmentNumber);
            if (id != 0)
            {
                return id;
            }

            var address = await _addressRepository.CreateAndGetAsync(new Address
            {
                Country = country,
                City = city,
                ZipCode = zipCode,
                Street = street,
                BuildingNumber = buildingNumber,
                ApartmentNumber = apartmentNumber
            });
            return address.Id;
        }
    }
}
