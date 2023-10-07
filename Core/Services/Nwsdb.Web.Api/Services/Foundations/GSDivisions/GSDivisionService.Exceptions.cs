//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.DSDevisions;
using Nwsdb.Web.Api.Models.DSDivisions.Exceptions;
using Nwsdb.Web.Api.Models.GSDivisions;
using Nwsdb.Web.Api.Models.GSDivisions.Exceptions;
using Xeptions;

namespace Nwsdb.Web.Api.Services.Foundations.GSDivisions
{
    public partial class GSDivisionService
    {
        private delegate IQueryable<GSDivision> ReturningGSDivisionsFunction();
        private delegate ValueTask<GSDivision> ReturningGSDivisionFunction();

        private IQueryable<GSDivision> TryCatch(ReturningGSDivisionsFunction returningGSDivisionsFunction)
        {
            try
            {
                return returningGSDivisionsFunction();
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

        private GSDivisionDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var dSDivisionDependencyException = new GSDivisionDependencyException(exception);
            this.loggingBroker.LogCritical(dSDivisionDependencyException);

            throw dSDivisionDependencyException;
        }

        private GSDivisionServiceException CreateAndLogServiceException(Xeption exception)
        {
            var dSDivisionServiceException = new GSDivisionServiceException(exception);
            this.loggingBroker.LogError(dSDivisionServiceException);

            throw dSDivisionServiceException;
        }
    }
}
