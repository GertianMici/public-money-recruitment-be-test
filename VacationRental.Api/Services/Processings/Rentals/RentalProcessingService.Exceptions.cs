using NetXceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Exceptions.Foundations.Rentals;
using VacationRental.Api.Models.Exceptions.Processings.Rentals;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Rentals
{
    public partial class RentalProcessingService
    {
        private delegate ValueTask<Rental> ReturningRentalFunction();
        private delegate ValueTask<ResourceIdViewModel> ReturningResourceIdFunction();
        private delegate IQueryable<Rental> ReturningRentalsFunction();

        private async ValueTask<Rental> TryCatch(ReturningRentalFunction returningRentalFunction)
        {
            try
            {
                return await returningRentalFunction();
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
            catch (RentalValidationException rentalValidationException)
            {
                var failedProcessingDependencyValidationException = 
                    new RentalProcessingDependencyValidationException(rentalValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (RentalDependencyException rentalDependencyException)
            {
                var failedProcessingDependencyException =
                    new RentalProcessingDependencyException(rentalDependencyException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (RentalDependencyValidationException rentalDependencyValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new RentalProcessingDependencyValidationException(rentalDependencyValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (RentalServiceException rentalServiceException)
            {
                var failedProcessingDependencyException =
                     new RentalProcessingDependencyException(rentalServiceException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (Exception exception)
            {
                var failedRentalProcessingServiceException =
                    new FailedRentalProcessingServiceException(exception);

                throw CreateAndLogProcessingServiceException(failedRentalProcessingServiceException);
            }
        }

        private async ValueTask<ResourceIdViewModel> TryCatch(ReturningResourceIdFunction returningResourceIdFunction)
        {
            try
            {
                return await returningResourceIdFunction();
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
            catch (RentalValidationException rentalValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new RentalProcessingDependencyValidationException(rentalValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (RentalDependencyException rentalDependencyException)
            {
                var failedProcessingDependencyException =
                    new RentalProcessingDependencyException(rentalDependencyException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (RentalDependencyValidationException rentalDependencyValidationException)
            {
                var failedProcessingDependencyValidationException =
                    new RentalProcessingDependencyValidationException(rentalDependencyValidationException);

                throw CreateAndLogProcessingDependencyValidationException(
                    failedProcessingDependencyValidationException);
            }
            catch (RentalServiceException rentalServiceException)
            {
                var failedProcessingDependencyException =
                     new RentalProcessingDependencyException(rentalServiceException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (Exception exception)
            {
                var failedRentalProcessingServiceException =
                    new FailedRentalProcessingServiceException(exception);

                throw CreateAndLogProcessingServiceException(failedRentalProcessingServiceException);
            }
        }

        private IQueryable<Rental> TryCatch(ReturningRentalsFunction returningRentalsFunction)
        {
            try
            {
                return returningRentalsFunction();
            }
            catch (RentalDependencyException rentalDependencyException)
            {
                var failedProcessingDependencyException =
                    new RentalProcessingDependencyException(rentalDependencyException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
            catch (RentalServiceException rentalServiceException)
            {
                var failedProcessingDependencyException =
                     new RentalProcessingDependencyException(rentalServiceException);

                throw CreateAndLogProcessingDependencyException(failedProcessingDependencyException);
            }
        }

        private RentalProcessingValidationException CreateAndLogProcessingValidationException(
            NetXception exception)
        {
            var rentalValidationException =
                new RentalProcessingValidationException(exception);

            this.loggingBroker.LogError(rentalValidationException);

            return rentalValidationException;
        }

        private RentalProcessingDependencyValidationException CreateAndLogProcessingDependencyValidationException(
            NetXception exception)
        {
            var rentalDependencyValidationException =
                new RentalProcessingDependencyValidationException(exception);

            this.loggingBroker.LogError(rentalDependencyValidationException);

            return rentalDependencyValidationException;
        }

        private RentalProcessingDependencyException CreateAndLogProcessingDependencyException(
            NetXception exception)
        {
            var rentalDependencyException = new RentalProcessingDependencyException(exception);
            this.loggingBroker.LogError(rentalDependencyException);

            return rentalDependencyException;
        }

        private RentalProcessingServiceException CreateAndLogProcessingServiceException(
            Exception exception)
        {
            var rentalServiceException = new RentalProcessingServiceException(exception);
            this.loggingBroker.LogError(rentalServiceException);

            return rentalServiceException;
        }
    }
}
