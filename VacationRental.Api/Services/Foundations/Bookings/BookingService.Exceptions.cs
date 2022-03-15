using Microsoft.EntityFrameworkCore;
using NetXceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Exceptions.Foundations.Bookings;

namespace VacationRental.Api.Services.Foundations.Bookings
{
    public partial class BookingService
    {
        private delegate IQueryable<Booking> ReturningBookingsFunction();
        private delegate ValueTask<Booking> ReturningBookingFunction();

        private async ValueTask<Booking> TryCatch(ReturningBookingFunction returningBookingFunction)
        {
            try
            {
                return await returningBookingFunction();
            }
            catch (NullBookingException nullBookingException)
            {
                throw CreateAndLogValidationException(nullBookingException);
            }
            catch (InvalidBookingException invalidBookingException)
            {
                throw CreateAndLogValidationException(invalidBookingException);
            }
            catch (OutOfMemoryException exception)
            {
                var failedBookingStorageException =
                    new FailedBookingStorageException(exception);

                throw CreateAndLogCriticalDependencyException(failedBookingStorageException);
            }
            catch (NotFoundBookingException notFoundBookingException)
            {
                throw CreateAndLogValidationException(notFoundBookingException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedBookingException = new LockedBookingException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedBookingException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedBookingStorageException =
                    new FailedBookingStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedBookingStorageException);
            }
            catch (Exception exception)
            {
                var failedBookingServiceException =
                    new FailedBookingServiceException(exception);

                throw CreateAndLogServiceException(failedBookingServiceException);
            }
        }

        private IQueryable<Booking> TryCatch(ReturningBookingsFunction returningBookingsFunction)
        {
            try
            {
                return returningBookingsFunction();
            }
            catch (OutOfMemoryException exception)
            {
                var failedBookingStorageException =
                    new FailedBookingStorageException(exception);

                throw CreateAndLogCriticalDependencyException(failedBookingStorageException);
            }
            catch (Exception exception)
            {
                var failedBookingServiceException =
                    new FailedBookingServiceException(exception);

                throw CreateAndLogServiceException(failedBookingServiceException);
            }
        }

        private BookingValidationException CreateAndLogValidationException(
            NetXception exception)
        {
            var bookingValidationException =
                new BookingValidationException(exception);

            this.loggingBroker.LogError(bookingValidationException);

            return bookingValidationException;
        }

        private BookingDependencyException CreateAndLogCriticalDependencyException(
            NetXception exception)
        {
            var bookingDependencyException = new BookingDependencyException(exception);
            this.loggingBroker.LogCritical(bookingDependencyException);

            return bookingDependencyException;
        }

        private BookingDependencyValidationException CreateAndLogDependencyValidationException(
            NetXception exception)
        {
            var bookingDependencyValidationException =
                new BookingDependencyValidationException(exception);

            this.loggingBroker.LogError(bookingDependencyValidationException);

            return bookingDependencyValidationException;
        }

        private BookingDependencyException CreateAndLogDependencyException(NetXception exception)
        {
            var bookingDependencyException = new BookingDependencyException(exception);
            this.loggingBroker.LogError(bookingDependencyException);

            return bookingDependencyException;
        }

        private BookingServiceException CreateAndLogServiceException(
            Exception exception)
        {
            var bookingServiceException = new BookingServiceException(exception);
            this.loggingBroker.LogError(bookingServiceException);

            return bookingServiceException;
        }
    }
}
