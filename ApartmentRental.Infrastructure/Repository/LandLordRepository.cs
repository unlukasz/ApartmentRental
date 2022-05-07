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
    internal class LandLordRepository : ILandLordRepository
    {
        private readonly MainContext _mainContext;

        public LandLordRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task Add(LandLord entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var landLordToDelete = await _mainContext.LandLord.SingleOrDefaultAsync(x => x.Id == id);
            if (landLordToDelete != null)
            {
                _mainContext.LandLord.Remove(landLordToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<LandLord>> GetAll()
        {
            var landLords = await _mainContext.LandLord.ToListAsync();

            foreach (var landLord in landLords)
            {
                await _mainContext.Entry(landLord).Reference(x => x.Account).LoadAsync();
            }
            return landLords;
        }

        public async Task<LandLord> GetById(int id)
        {
            var landLords = await _mainContext.LandLord.SingleOrDefaultAsync(x => x.Id == id);
            if (landLords != null)
            {
                await _mainContext.Entry(landLords).Reference(x => x.Account).LoadAsync();
                return landLords;
            }

            throw new EntityNotFoundException();
        }

        public async Task Update(LandLord entity)
        {
            var landLordToUpdate = await _mainContext.LandLord.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (landLordToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            landLordToUpdate.Account = entity.Account;
            landLordToUpdate.DateOfUpdates = DateTime.UtcNow;
            
            await _mainContext.SaveChangesAsync();
        }
    }
}
