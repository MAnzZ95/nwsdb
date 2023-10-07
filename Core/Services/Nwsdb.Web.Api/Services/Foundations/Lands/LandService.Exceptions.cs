//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Xeptions;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.Lands.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.Lands
{
    public partial class LandService
    {
        private delegate IQueryable<Land> ReturningLandsFunction();
        private delegate ValueTask<Land> ReturningLandFunction();

        private IQueryable<Land> TryCatch(ReturningLandsFunction returningLandsFunction)
        {
            try
            {
                return returningLandsFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedLandStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedLandServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private async ValueTask<Land> TryCatch(ReturningLandFunction returningLandFunction)
        {
            try
            {
                return await returningLandFunction();
            }
            catch (NullLandHistoryException nullLandException)
            {
                throw CreateAndLogValidationException(nullLandException);
            }
            catch (InvalidLandHistoryException invalidLandException)
            {
                throw CreateAndLogValidationException(invalidLandException);
            }
            catch (NotFoundLandHistoryException notFoundLandException)
            {
                throw CreateAndLogValidationException(notFoundLandException);
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedLandStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedLandServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private LandHistoryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new LandDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private LandHistoryDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var landDependencyException = new LandDependencyException(exception);
            this.loggingBroker.LogError(landDependencyException);

            throw landDependencyException;
        }
        private LandHistoryServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new LandServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }

        private LandHistoryValidationException CreateAndLogValidationException(Xeption exception)
        {
            var landValidationException = new LandValidationException(exception);
            this.loggingBroker.LogError(landValidationException);

            throw landValidationException;
        }

        private LandTypeDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var landDependencyValidationException = new LandDependencyValidationException(exception);
            this.loggingBroker.LogError(landDependencyValidationException);

            throw landDependencyValidationException;
        }
    }
}
