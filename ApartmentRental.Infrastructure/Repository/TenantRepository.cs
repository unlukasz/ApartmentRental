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
    internal class TenantRepository : ITenantRepository
    {
        private readonly MainContext _mainContext;

        public TenantRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task Add(Tenant entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var tenantToDelete = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
            if (tenantToDelete != null)
            {
                _mainContext.Tenant.Remove(tenantToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Tenant>> GetAll()
        {
            var tenants = await _mainContext.Tenant.ToListAsync();

            foreach (var tenant in tenants)
            {
                await _mainContext.Entry(tenant).Reference(x => x.Account).LoadAsync();
            }
            return tenants;
        }

        public async Task<Tenant> GetById(int id)
        {
            var tenants = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
            if (tenants != null)
            {
                await _mainContext.Entry(tenants).Reference(x => x.Account).LoadAsync();
                return tenants;
            }

            throw new EntityNotFoundException();
        }

        public async Task Update(Tenant entity)
        {
            var tenantToUpdate = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (tenantToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            tenantToUpdate.Account = entity.Account;
            tenantToUpdate.DateOfUpdates = entity.DateOfUpdates;

            await _mainContext.SaveChangesAsync();
        }
    }
}
