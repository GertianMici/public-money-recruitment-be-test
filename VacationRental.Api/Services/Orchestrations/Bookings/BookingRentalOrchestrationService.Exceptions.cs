using NetXceptions;
using System;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.Models.Calendars.Exceptons;
using VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals;
using VacationRental.Api.Models.Exceptions.Processings.Bookings;
using VacationRental.Api.Models.Exceptions.Processings.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations.Bookings
{
    public partial class BookingRentalOrchestrationService
    {
        private delegate ValueTask<Booking> ReturningBookingFunction();
        private delegate ValueTask<ResourceIdViewModel> ReturningResourceIdFunction();
        private delegate ValueTask<Calendar> ReturningCalendarFunction();

        private async ValueTask<Booking> TryCatch(
            ReturningBookingFunction returningBookingFunction)
        {
            try
            {
                return await returningBookingFunction();
            }
            catch (NullBookingException nullBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(invalidBookingException);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(notFoundBookingException);
            }
            catch (RentalNotAvailableException rentalNotAvailableException)
            {
                throw CreateAndLogOrchestrationValidationException(rentalNotAvailableException);
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(nullRentalException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(invalidRentalException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(notFoundRentalException);
            }
            catch (BookingProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogOrchestrationServiceException(exception);
            }
        }

        private async ValueTask<Calendar> TryCatch(
            ReturningCalendarFunction returningCalendarFunction)
        {
            try
            {
                return await returningCalendarFunction();
            }
            catch (NullBookingException nullBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(invalidBookingException);
            }
            catch (InvalidCalendarParameters invalidCalendarParameters)
            {
                throw CreateAndLogOrchestrationValidationException(invalidCalendarParameters);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(notFoundBookingException);
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(nullRentalException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(invalidRentalException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(notFoundRentalException);
            }
            catch (BookingProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogOrchestrationServiceException(exception);
            }
        }

        private async ValueTask<ResourceIdViewModel> TryCatch(
            ReturningResourceIdFunction returningResourceIdFunction)
        {
            try
            {
                return await returningResourceIdFunction();
            }
            catch (NullBookingException nullBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(invalidBookingException);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogOrchestrationValidationException(notFoundBookingException);
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(nullRentalException);
            }
            catch (RentalNotAvailableException rentalNotAvailableException)
            {
                throw CreateAndLogOrchestrationValidationException(rentalNotAvailableException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(invalidRentalException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogOrchestrationValidationException(notFoundRentalException);
            }
            catch (BookingProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogOrchestrationServiceException(exception);
            }
        }

        private BookingRentalOrchestrationDependencyValidationException CreateAndLogDependencyValidationException(
            NetXception exception)
        {
            var bookingRentalOrchestrationDependencyValidationException =
                new BookingRentalOrchestrationDependencyValidationException(exception.InnerException as NetXception);

            this.loggingBroker.LogError(bookingRentalOrchestrationDependencyValidationException);

            throw bookingRentalOrchestrationDependencyValidationException;
        }

        private BookingRentalOrchestrationServiceException CreateAndLogOrchestrationServiceException(
            Exception exception)
        {
            var bookingRentalOrchestrationServiceException =
                new BookingRentalOrchestrationServiceException(exception);

            this.loggingBroker.LogError(bookingRentalOrchestrationServiceException);

            return bookingRentalOrchestrationServiceException;
        }

        private BookingRentalOrchestrationValidationException CreateAndLogOrchestrationValidationException(
            NetXception exception)
        {
            var bookingRentalProcessingValidationException =
                new BookingRentalOrchestrationValidationException(exception);

            this.loggingBroker.LogError(bookingRentalProcessingValidationException);

            return bookingRentalProcessingValidationException;
        }
    }
}
