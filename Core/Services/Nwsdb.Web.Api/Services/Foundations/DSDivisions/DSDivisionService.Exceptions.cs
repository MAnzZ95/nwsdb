//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.DSDevisions;
using Nwsdb.Web.Api.Models.DSDivisions.Exceptions;
using Xeptions;

namespace Nwsdb.Web.Api.Services.Foundations.DSDivisions
{
    public partial class DSDivisionService
    {
        private delegate IQueryable<DSDivision> ReturningDSDivisionsFunction();
        private delegate ValueTask<DSDivision> ReturningDSDivisionFunction();

        private IQueryable<DSDivision> TryCatch(ReturningDSDivisionsFunction returningDSDivisionsFunction)
        {
            try
            {
                return returningDSDivisionsFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedDSDivisionStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedDSDivisionServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private DSDivisionDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var dSDivisionDependencyException = new DSDivisionDependencyException(exception);
            this.loggingBroker.LogCritical(dSDivisionDependencyException);

            throw dSDivisionDependencyException;
        }

        private DSDivisionServiceException CreateAndLogServiceException(Xeption exception)
        {
            var dSDivisionServiceException = new DSDivisionServiceException(exception);
            this.loggingBroker.LogError(dSDivisionServiceException);

            throw dSDivisionServiceException;
        }
    }
}
