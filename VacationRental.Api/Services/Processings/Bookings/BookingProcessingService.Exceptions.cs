using NetXceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Exceptions.Foundations.Bookings;
using VacationRental.Api.Models.Exceptions.Processings.Bookings;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Bookings
{
    public partial class BookingProcessingService
    {
        private delegate ValueTask<Booking> ReturningBookingFunction();
        private delegate ValueTask<ResourceIdViewModel> ReturningResourceIdFunction();
        private delegate IQueryable<Booking> ReturningBookingsFunction();

        private async ValueTask<Booking> TryCatch(ReturningBookingFunction returningBookingFunction)
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
            catch (BookingValidationException bookingValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new BookingProcessingDependencyValidationException(bookingValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (BookingDependencyException bookingDependencyException)
            {
                var failedProcessingDependencyException =
                    new BookingProcessingDependencyException(bookingDependencyException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (BookingDependencyValidationException bookingDependencyValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new BookingProcessingDependencyValidationException(bookingDependencyValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (BookingServiceException bookingServiceException)
            {
                var failedProcessingDependencyException =
                     new BookingProcessingDependencyException(bookingServiceException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (Exception exception)
            {
                var failedBookingProcessingServiceException =
                    new FailedBookingProcessingServiceException(exception);

                throw CreateAndLogProcessingServiceException(failedBookingProcessingServiceException);
            }
        }

        private async ValueTask<ResourceIdViewModel> TryCatch(ReturningResourceIdFunction returningResourceIdFunction)
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
            catch (BookingValidationException bookingValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new BookingProcessingDependencyValidationException(bookingValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (BookingDependencyException bookingDependencyException)
            {
                var failedProcessingDependencyException =
                    new BookingProcessingDependencyException(bookingDependencyException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (BookingDependencyValidationException bookingDependencyValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new BookingProcessingDependencyValidationException(bookingDependencyValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (BookingServiceException bookingServiceException)
            {
                var failedProcessingDependencyException =
                     new BookingProcessingDependencyException(bookingServiceException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (Exception exception)
            {
                var failedBookingProcessingServiceException =
                    new FailedBookingProcessingServiceException(exception);

                throw CreateAndLogProcessingServiceException(failedBookingProcessingServiceException);
            }
        }


        private IQueryable<Booking> TryCatch(ReturningBookingsFunction returningBookingsFunction)
        {
            try
            {
                return returningBookingsFunction();
            }
            catch (BookingDependencyException bookingDependencyException)
            {
                var failedProcessingDependencyException =
                    new BookingProcessingDependencyException(bookingDependencyException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (BookingServiceException bookingServiceException)
            {
                var failedProcessingDependencyException =
                     new BookingProcessingDependencyException(bookingServiceException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
        }


        private BookingProcessingValidationException CreateAndLogProcessingValidationException(
            NetXception exception)
        {
            var bookingValidationException =
                new BookingProcessingValidationException(exception);

            this.loggingBroker.LogError(bookingValidationException);

            return bookingValidationException;
        }

        private BookingProcessingDependencyValidationException CreateAndLogProcessingDependencyValidationException(
            NetXception exception)
        {
            var bookingDependencyValidationException =
                new BookingProcessingDependencyValidationException(exception);

            this.loggingBroker.LogError(bookingDependencyValidationException);

            return bookingDependencyValidationException;
        }

        private BookingProcessingDependencyException CreateAndLogProcessingDependencyException(
            NetXception exception)
        {
            var bookingDependencyException = new BookingProcessingDependencyException(exception);
            this.loggingBroker.LogError(bookingDependencyException);

            return bookingDependencyException;
        }

        private BookingProcessingServiceException CreateAndLogProcessingServiceException(
            Exception exception)
        {
            var bookingServiceException = new BookingProcessingServiceException(exception);
            this.loggingBroker.LogError(bookingServiceException);

            return bookingServiceException;
        }
    }
}
