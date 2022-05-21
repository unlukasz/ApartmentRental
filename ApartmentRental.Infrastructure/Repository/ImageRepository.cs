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
    public class ImageRepository : IImageRepository
    {
        private readonly MainContext _mainContext;

        public ImageRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task Add(Image entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var imageToDelete = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == id);
            if (imageToDelete != null)
            {
                _mainContext.Image.Remove(imageToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            var images = await _mainContext.Image.ToListAsync();

            foreach (var image in images)
            {
                await _mainContext.Entry(image).Reference(x => x.Apartment).LoadAsync();
            }
            return images;
        }

        public async Task<Image> GetById(int id)
        {
            var images = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == id);
            if (images != null)
            {
                await _mainContext.Entry(images).Reference(x => x.Apartment).LoadAsync();
                return images;
            }

            throw new EntityNotFoundException();
        }

        public async Task Update(Image entity)
        {
            var imageToUpdate = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (imageToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            imageToUpdate.Apartment = entity.Apartment;
            imageToUpdate.DateOfUpdates = DateTime.UtcNow;

            await _mainContext.SaveChangesAsync();
        }
    }
}
