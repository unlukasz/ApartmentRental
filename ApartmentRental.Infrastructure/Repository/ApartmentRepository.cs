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
    internal class ApartmentRepository : IApartmentRepository
    {
        private readonly MainContext _mainContext;

        public ApartmentRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

         async Task IRepository<Apartment>.Add(Apartment entity)
        {
            var apartmentToAdd = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (apartmentToAdd != null)
            {
                entity.DateOfCreation = DateTime.UtcNow;
                await _mainContext.AddAsync(entity);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        async Task IRepository<Apartment>.DeleteById(int id)
        {
            var apartmentToDelete = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == id);
            if(apartmentToDelete != null)
            {
                _mainContext.Apartment.Remove(apartmentToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        async Task<IEnumerable<Apartment>> IRepository<Apartment>.GetAll()
        {
            var apartments = await _mainContext.Apartment.ToListAsync();

            foreach(var apartment in apartments)
            {
                await _mainContext.Entry(apartment).Reference(x => x.Address).LoadAsync();
            }
            return apartments;
        }

        async Task<Apartment> IRepository<Apartment>.GetById(int id)
        {
            var apartments = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == id);
            if(apartments != null)
            {
                await _mainContext.Entry(apartments).Reference(x => x.Address).LoadAsync();
                return apartments;
            }

            throw new EntityNotFoundException();
        }

        async Task IRepository<Apartment>.Update(Apartment entity)
        {
            var apartmentToUpdate = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if(apartmentToUpdate == null) 
            { 
                throw new EntityNotFoundException(); 
            }
            
            apartmentToUpdate.Floor = entity.Floor;
            apartmentToUpdate.IsElevator = entity.IsElevator;
            apartmentToUpdate.RentAmount = entity.RentAmount;
            apartmentToUpdate.SquareMeters = entity.SquareMeters;
            apartmentToUpdate.NumberOfRooms = entity.NumberOfRooms;
            apartmentToUpdate.DateOfUpdates = DateTime.UtcNow;

            await _mainContext.SaveChangesAsync();
        }
    }
}
