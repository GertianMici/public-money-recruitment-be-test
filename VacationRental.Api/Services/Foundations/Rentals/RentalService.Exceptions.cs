using Microsoft.EntityFrameworkCore;
using NetXceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;

namespace VacationRental.Api.Services.Foundations.Rentals
{
    public partial class RentalService
    {
        private delegate IQueryable<Rental> ReturningRentalsFunction();
        private delegate ValueTask<Rental> ReturningRentalFunction();

        private async ValueTask<Rental> TryCatch(ReturningRentalFunction returningRentalFunction)
        {
            try
            {
                return await returningRentalFunction();
            }
            catch (NullRentalException nullRentalException)
            {
                throw CreateAndLogValidationException(nullRentalException);
            }
            catch (InvalidRentalException invalidRentalException)
            {
                throw CreateAndLogValidationException(invalidRentalException);
            }
            catch (OutOfMemoryException exception)
            {
                var failedRentalStorageException =
                    new FailedRentalStorageException(exception);

                throw CreateAndLogCriticalDependencyException(failedRentalStorageException);
            }
            catch (NotFoundRentalException notFoundRentalException)
            {
                throw CreateAndLogValidationException(notFoundRentalException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedRentalException = new LockedRentalException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedRentalException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedRentalStorageException =
                    new FailedRentalStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedRentalStorageException);
            }
            catch (Exception exception)
            {
                var failedRentalServiceException =
                    new FailedRentalServiceException(exception);

                throw CreateAndLogServiceException(failedRentalServiceException);
            }
        }

        private IQueryable<Rental> TryCatch(ReturningRentalsFunction returningRentalsFunction)
        {
            try
            {
                return returningRentalsFunction();
            }
            catch (OutOfMemoryException exception)
            {
                var failedRentalStorageException =
                    new FailedRentalStorageException(exception);

                throw CreateAndLogCriticalDependencyException(failedRentalStorageException);
            }
            catch (Exception exception)
            {
                var failedRentalServiceException =
                    new FailedRentalServiceException(exception);

                throw CreateAndLogServiceException(failedRentalServiceException);
            }
        }

        private RentalValidationException CreateAndLogValidationException(
            NetXception exception)
        {
            var rentalValidationException =
                new RentalValidationException(exception);

            this.loggingBroker.LogError(rentalValidationException);

            return rentalValidationException;
        }

        private RentalDependencyException CreateAndLogCriticalDependencyException(
            NetXception exception)
        {
            var rentalDependencyException = new RentalDependencyException(exception);
            this.loggingBroker.LogCritical(rentalDependencyException);

            return rentalDependencyException;
        }

        private RentalDependencyValidationException CreateAndLogDependencyValidationException(
            NetXception exception)
        {
            var rentalDependencyValidationException =
                new RentalDependencyValidationException(exception);

            this.loggingBroker.LogError(rentalDependencyValidationException);

            return rentalDependencyValidationException;
        }

        private RentalDependencyException CreateAndLogDependencyException(NetXception exception)
        {
            var rentalDependencyException = new RentalDependencyException(exception);
            this.loggingBroker.LogError(rentalDependencyException);

            return rentalDependencyException;
        }

        private RentalServiceException CreateAndLogServiceException(
            Exception exception)
        {
            var rentalServiceException = new RentalServiceException(exception);
            this.loggingBroker.LogError(rentalServiceException);

            return rentalServiceException;
        }
    }
}
