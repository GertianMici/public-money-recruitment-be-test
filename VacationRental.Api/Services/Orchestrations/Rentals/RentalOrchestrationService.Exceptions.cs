using NetXceptions;
using System;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Exceptions.Orchestrations.Rentals;
using VacationRental.Api.Models.Exceptions.Processings.Bookings;
using VacationRental.Api.Models.Exceptions.Processings.Rentals;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations.Rentals
{
    public partial class RentalOrchestrationService
    {
        private delegate ValueTask<Rental> ReturningRentalFunction();
        private delegate ValueTask<ResourceIdViewModel> ReturningResourceIdFunction();


        private async ValueTask<Rental> TryCatch(
            ReturningRentalFunction returningRentalFunction)
        {
            try
            {
                return await returningRentalFunction();
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
            catch (RentalProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingDependencyValidationException exception)
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
            catch (RentalProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (RentalProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (BookingProcessingDependencyValidationException exception)
            {
                throw CreateAndLogDependencyValidationException(exception);
            }
            catch (Exception exception)
            {
                throw CreateAndLogOrchestrationServiceException(exception);
            }
        }

        private RentalOrchestrationDependencyValidationException CreateAndLogDependencyValidationException(
            NetXception exception)
        {
            var rentalOrchestrationDependencyValidationException =
                new RentalOrchestrationDependencyValidationException(exception.InnerException as NetXception);

            this.loggingBroker.LogError(rentalOrchestrationDependencyValidationException);

            throw rentalOrchestrationDependencyValidationException;
        }

        private RentalOrchestrationServiceException CreateAndLogOrchestrationServiceException(
            Exception exception)
        {
            var rentalOrchestrationServiceException =
                new RentalOrchestrationServiceException(exception);

            this.loggingBroker.LogError(rentalOrchestrationServiceException);

            return rentalOrchestrationServiceException;
        }

        private RentalOrchestrationValidationException CreateAndLogOrchestrationValidationException(
            NetXception exception)
        {
            var rentalOrchestrationValidationException =
                new RentalOrchestrationValidationException(exception);

            this.loggingBroker.LogError(rentalOrchestrationValidationException);

            return rentalOrchestrationValidationException;
        }
    }
}
