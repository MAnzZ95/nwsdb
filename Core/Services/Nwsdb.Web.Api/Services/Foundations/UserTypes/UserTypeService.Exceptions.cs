//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Models.Users;
using Xeptions;
using Nwsdb.Web.Api.Models.UserTypes;
using Nwsdb.Web.Api.Models.UserTypes.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.UserTypes
{
    public partial class UserTypeService
    {
        private delegate IQueryable<UserType> ReturningUserTypesFunction();
        private delegate ValueTask<UserType> ReturningUserTypeFunction();

        private IQueryable<UserType> TryCatch(ReturningUserTypesFunction returningUserTypesFunction)
        {
            try
            {
                return returningUserTypesFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedUserTypeStorageException = new FailedUserTypeStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedUserTypeStorageException);
            }
            catch (Exception exception)
            {
                var failedUserTypeServiceException = new FailedUserTypeServiceException(exception);

                throw CreateAndLogServiceException(failedUserTypeServiceException);
            }
        }

        private async ValueTask<UserType> TryCatch(ReturningUserTypeFunction returningUserTypeFunction)
        {
            try
            {
                return await returningUserTypeFunction();
            }
            catch (NullUserTypeException nullUserTypeException)
            {
                throw CreateAndLogValidationException(nullUserTypeException);
            }
            catch (InvalidUserTypeException invalidUserTypeException)
            {
                throw CreateAndLogValidationException(invalidUserTypeException);
            }
            catch (NotFoundUserTypeException notFoundUserTypeException)
            {
                throw CreateAndLogValidationException(notFoundUserTypeException);
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedUserTypeStorageException = new FailedUserTypeStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedUserTypeStorageException);
            }
            catch (Exception exception)
            {
                var failedUserTypeServiceException = new FailedUserTypeServiceException(exception);

                throw CreateAndLogServiceException(failedUserTypeServiceException);
            }
        }

        private UserTypeDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var userTypeDependencyException = new UserTypeDependencyException(exception);
            this.loggingBroker.LogCritical(userTypeDependencyException);

            throw userTypeDependencyException;
        }

        private UserTypeDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var userTypeDependencyException = new UserTypeDependencyException(exception);
            this.loggingBroker.LogError(userTypeDependencyException);

            throw userTypeDependencyException;
        }
        private UserTypeServiceException CreateAndLogServiceException(Xeption exception)
        {
            var userTypeServiceException = new UserTypeServiceException(exception);
            this.loggingBroker.LogError(userTypeServiceException);

            throw userTypeServiceException;
        }

        private UserTypeValidationException CreateAndLogValidationException(Xeption exception)
        {
            var userTypeValidationException = new UserTypeValidationException(exception);
            this.loggingBroker.LogError(userTypeValidationException);

            throw userTypeValidationException;
        }

        private UserTypeDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var userTypeDependencyValidationException = new UserTypeDependencyValidationException(exception);
            this.loggingBroker.LogError(userTypeDependencyValidationException);

            throw userTypeDependencyValidationException;
        }
    }
}
