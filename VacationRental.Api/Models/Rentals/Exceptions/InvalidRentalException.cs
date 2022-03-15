﻿using NetXceptions;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class InvalidRentalException : NetXception
    {
        public InvalidRentalException(string message) : base(message)
        { }

        public InvalidRentalException()
           : base(message: "Invalid rental. Please correct the errors and try again.")
        { }
    }
}
