using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Rentals
{
    public class LockedRentalException : NetXception
    {
        public LockedRentalException(Exception innerException)
            : base(message: "Locked rental record exception, please try again later", innerException)
        { }
    }
}
