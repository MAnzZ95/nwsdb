//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.LandTypes;
using Xeptions;
using Nwsdb.Web.Api.Models.LandTypes.Exceptions;
using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Models.Users;

namespace Nwsdb.Web.Api.Services.Foundations.LandTypes
{
    public partial class LandTypeService
    {
        private delegate IQueryable<LandType> ReturningLandTypesFunction();
        private delegate ValueTask<LandType> ReturningLandTypeFunction();

        private IQueryable<LandType> TryCatch(ReturningLandTypesFunction returningLandTypesFunction)
        {
            try
            {
                return returningLandTypesFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedLandTypeStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedLandTypeServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private async ValueTask<LandType> TryCatch(ReturningLandTypeFunction returningLandTypeFunction)
        {
            try
            {
                return await returningLandTypeFunction();
            }
            catch (NullLandTypeException nullLandTypeException)
            {
                throw CreateAndLogValidationException(nullLandTypeException);
            }
            catch (InvalidUserException invalidLandTypeException)
            {
                throw CreateAndLogValidationException(invalidLandTypeException);
            }
            catch (NotFoundLandTypeException notFoundLandTypeException)
            {
                throw CreateAndLogValidationException(notFoundLandTypeException);
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandTypeStorageException = new FailedLandTypeStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandTypeStorageException);
            }
            catch (Exception exception)
            {
                var failedLandTypeServiceException = new FailedLandTypeServiceException(exception);

                throw CreateAndLogServiceException(failedLandTypeServiceException);
            }
        }

        private LandTypeDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new LandTypeDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private LandTypeServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new LandTypeServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }

        private LandTypeValidationException CreateAndLogValidationException(Xeption exception)
        {
            var landTypeValidationException = new LandTypeValidationException(exception);
            this.loggingBroker.LogError(landTypeValidationException);

            throw landTypeValidationException;
        }

        private LandTypeDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var landTypeDependencyValidationException = new LandTypeDependencyValidationException(exception);
            this.loggingBroker.LogError(landTypeDependencyValidationException);

            throw landTypeDependencyValidationException;
        }
    }
}
