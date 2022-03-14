using NetXceptions;
using System;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals;
using VacationRental.Api.Models.Exceptions.Processings.Bookings;
using VacationRental.Api.Models.Exceptions.Processings.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations
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
                throw CreateAndLogProcessingValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogProcessingValidationException(invalidBookingException);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogProcessingValidationException(notFoundBookingException);
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogProcessingValidationException(nullRentalException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogProcessingValidationException(invalidRentalException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogProcessingValidationException(notFoundRentalException);
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
                throw CreateAndLogProcessingServiceException(exception);
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
                throw CreateAndLogProcessingValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogProcessingValidationException(invalidBookingException);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogProcessingValidationException(notFoundBookingException);
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogProcessingValidationException(nullRentalException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogProcessingValidationException(invalidRentalException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogProcessingValidationException(notFoundRentalException);
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
                throw CreateAndLogProcessingServiceException(exception);
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
                throw CreateAndLogProcessingValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogProcessingValidationException(invalidBookingException);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogProcessingValidationException(notFoundBookingException);
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogProcessingValidationException(nullRentalException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogProcessingValidationException(invalidRentalException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogProcessingValidationException(notFoundRentalException);
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
                throw CreateAndLogProcessingServiceException(exception);
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

        private BookingRentalOrchestrationServiceException CreateAndLogProcessingServiceException(
            Exception exception)
        {
            var bookingRentalOrchestrationServiceException = 
                new BookingRentalOrchestrationServiceException(exception);

            this.loggingBroker.LogError(bookingRentalOrchestrationServiceException);

            return bookingRentalOrchestrationServiceException;
        }

        private BookingRentalOrchestrationValidationException CreateAndLogProcessingValidationException(
            NetXception exception)
        {
            var bookingRentalProcessingValidationException =
                new BookingRentalOrchestrationValidationException(exception);

            this.loggingBroker.LogError(bookingRentalProcessingValidationException);

            return bookingRentalProcessingValidationException;
        }
    }
}
