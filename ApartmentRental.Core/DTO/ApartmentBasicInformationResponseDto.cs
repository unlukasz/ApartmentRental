﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Core.DTO
{
    public class ApartmentBasicInformationResponseDto
    {
        public decimal RentAmount { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal SquareMeters { get; set; }
        public bool IsElevatorInBuilding { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public ApartmentBasicInformationResponseDto(decimal rentAmount, int numberOfRooms, decimal squareMeters, bool isElevatorInBuilding, string city, string street)
        {
            RentAmount = rentAmount;
            NumberOfRooms = numberOfRooms;
            SquareMeters = squareMeters;
            IsElevatorInBuilding = isElevatorInBuilding;
            City = city;
            Street = street;
        }
    }
}
