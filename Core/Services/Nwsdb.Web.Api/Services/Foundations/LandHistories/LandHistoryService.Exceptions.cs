//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Models.Lands;
using Xeptions;
using Nwsdb.Web.Api.Models.LandHistories;
using Nwsdb.Web.Api.Models.LandHistories.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.LandHistories
{
    public partial class LandHistoryService
    {
        private delegate IQueryable<LandHistory> ReturningLandHistoriesFunction();
        private delegate ValueTask<LandHistory> ReturningLandHistoryFunction();

        private IQueryable<LandHistory> TryCatch(ReturningLandHistoriesFunction returningLandHistoriesFunction)
        {
            try
            {
                return returningLandHistoriesFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedLandHistoryStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedLandHistoryServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private async ValueTask<LandHistory> TryCatch(ReturningLandHistoryFunction returningLandHistoryFunction)
        {
            try
            {
                return await returningLandHistoryFunction();
            }
            catch (NullLandHistoryException nullLandHistoryException)
            {
                throw CreateAndLogValidationException(nullLandHistoryException);
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
                var failedLandStorageException = new FailedLandHistoryStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedLandHistoryServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private LandHistoryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new LandHistoryDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private LandHistoryDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var landDependencyException = new LandHistoryDependencyException(exception);
            this.loggingBroker.LogError(landDependencyException);

            throw landDependencyException;
        }
        private LandHistoryServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new LandHistoryServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }

        private LandHistoryValidationException CreateAndLogValidationException(Xeption exception)
        {
            var landValidationException = new LandHistoryValidationException(exception);
            this.loggingBroker.LogError(landValidationException);

            throw landValidationException;
        }

        private LandHistoryDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var landDependencyValidationException = new LandHistoryDependencyValidationException(exception);
            this.loggingBroker.LogError(landDependencyValidationException);

            throw landDependencyValidationException;
        }
    }
}
