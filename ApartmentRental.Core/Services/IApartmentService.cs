using ApartmentRental.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Core.Services
{
    public interface IApartmentService
    {
        Task<IEnumerable<ApartmentBasicInformationResponseDto>> GetAllApartmensBasinInfosAsync();
        Task AddNewApartmentToExistingLandLordAsync(ApartmentCreationRequestDto dto);
        Task<ApartmentBasicInformationResponseDto> GetTheCheapestApartmentAsync();
    }
}
