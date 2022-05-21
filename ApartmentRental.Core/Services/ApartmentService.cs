using ApartmentRental.Core.DTO;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Core.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ILandLordRepository _landLordRepository;
        private readonly IAddressService _addressService;
        public ApartmentService(IApartmentRepository apartmentRepository, ILandLordRepository landLordRepository, IAddressService addressService)
        {
            _apartmentRepository = apartmentRepository;
            _landLordRepository = landLordRepository;
            _addressService = addressService;
        }
        public async Task<IEnumerable<ApartmentBasicInformationResponseDto>> GetAllApartmensBasinInfosAsync()
        {
            var apartments = await _apartmentRepository.GetAll();
            return apartments.Select(x => new ApartmentBasicInformationResponseDto(
                x.RentAmount,
                x.NumberOfRooms,
                x.SquareMeters,
                x.IsElevator,
                x.Address.City,
                x.Address.Street));
        }
        public async Task<ApartmentBasicInformationResponseDto?> GetTheCheapestApartmentAsync()
        {
            var apartments = await _apartmentRepository.GetAll();
            var cheapestOne = apartments.MinBy(x => x.RentAmount);

            if (cheapestOne is null) return null;

            return new ApartmentBasicInformationResponseDto(
                cheapestOne.RentAmount,
                cheapestOne.NumberOfRooms,
                cheapestOne.SquareMeters,
                cheapestOne.IsElevator,
                cheapestOne.Address.City,
                cheapestOne.Address.Street);
        }

        public async Task AddNewApartmentToExistingLandLordAsync(ApartmentCreationRequestDto dto)
        {
            var landlord = await _landLordRepository.GetById(dto.LandLordId);
            var addressId = await _addressService.GetAddressIdOrCreateAsync(dto.Country, dto.City, 
                dto.ZipCode, dto.Street, dto.BuildingNumber, dto.ApartmentNumber);

            await _apartmentRepository.Add(new Apartment
            {
                AddressId = addressId,
                Floor = dto.Floor,
                LandLord = landlord,
                IsElevator = dto.IsElevator,
                RentAmount = dto.RentAmount,
                SquareMeters = dto.SquareMeters,
                NumberOfRooms = dto.NumberOfRooms
            });
        }
    }
}
