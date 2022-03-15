﻿using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Processings.Bookings
{
    public class BookingProcessingValidationException : NetXception
    {
        public BookingProcessingValidationException(NetXception innerException)
            : base(message: "Booking validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
