using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Processings.Bookings
{
    public class FailedBookingProcessingServiceException : NetXception
    {
        public FailedBookingProcessingServiceException(Exception innerException)
            : base(message: "Failed booking processing service occurred, please contact support", innerException)
        { }
    }
}
