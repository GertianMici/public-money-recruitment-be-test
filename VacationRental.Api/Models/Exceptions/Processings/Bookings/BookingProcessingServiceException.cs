﻿using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Processings.Bookings
{
    public class BookingProcessingServiceException : NetXception
    {
        public BookingProcessingServiceException(Exception innerException)
            : base(message: "Booking service error occurred, contact support.", innerException) { }
    }
}
