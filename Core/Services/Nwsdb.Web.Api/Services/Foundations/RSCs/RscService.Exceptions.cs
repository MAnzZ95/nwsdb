//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.RMOs.Exceptions;
using Nwsdb.Web.Api.Models.RMOs;
using Xeptions;
using Nwsdb.Web.Api.Models.RSCs;
using Nwsdb.Web.Api.Models.RSCs.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.RSCs
{
    public partial class RscService
    {
        private delegate IQueryable<Rsc> ReturningRscsFunction();
        private delegate ValueTask<Rsc> ReturningRscFunction();

        private IQueryable<Rsc> TryCatch(ReturningRscsFunction returningRmosFunction)
        {
            try
            {
                return returningRmosFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedRscStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedRscServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private RscDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new RscDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private RscServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new RscServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }
    }
}
