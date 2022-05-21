using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MainContext _mainContext;

        public AddressRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task Add(Address entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var addressToDelete = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
            if (addressToDelete != null)
            {
                _mainContext.Address.Remove(addressToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            var addresses = await _mainContext.Address.ToListAsync();

            foreach (var address in addresses)
            {
                await _mainContext.Entry(address).Reference(x => x.Street).LoadAsync();
            }
            return addresses;
        }

        public async Task<Address> GetById(int id)
        {
            var addresses = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
            if (addresses != null)
            {
                await _mainContext.Entry(addresses).Reference(x => x.Street).LoadAsync();
                return addresses;
            }

            throw new EntityNotFoundException();
        }

        public async Task Update(Address entity)
        {
            var addressToUpdate = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (addressToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            addressToUpdate.Street = entity.Street;
            addressToUpdate.ApartmentNumber = entity.ApartmentNumber;
            addressToUpdate.BuildingNumber = entity.BuildingNumber;
            addressToUpdate.City = entity.City;
            addressToUpdate.ZipCode = entity.ZipCode;
            addressToUpdate.Country = entity.Country;

            await _mainContext.SaveChangesAsync();
        }

        public async Task<int> GetAddressIdByItsAttributesAsync(string country, string city, string zipCode, string street, string buildingNumber, string apartmentNumber)
        {
            var adress = await _mainContext.Address.FirstOrDefaultAsync(x =>
            x.Country == country && x.City == city && x.ZipCode == zipCode && x.Street == street &&
            x.BuildingNumber == buildingNumber && x.ApartmentNumber == apartmentNumber);

            return adress?.Id ?? 0;
        }

        public async Task<Address> CreateAndGetAsync(Address address)
        {
            address.DateOfCreation = DateTime.UtcNow;
            address.DateOfUpdates = DateTime.UtcNow;
            await _mainContext.AddAsync(address);
            await _mainContext.SaveChangesAsync();

            return address;
        }
    }
}
