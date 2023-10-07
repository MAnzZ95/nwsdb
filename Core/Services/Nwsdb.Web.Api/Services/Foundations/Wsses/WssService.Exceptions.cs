//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.RSCs.Exceptions;
using Nwsdb.Web.Api.Models.RSCs;
using Xeptions;
using Nwsdb.Web.Api.Models.WSSs;
using Nwsdb.Web.Api.Models.WSSs.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.Wsses
{
    public partial class WssService
    {
        private delegate IQueryable<Wss> ReturningWssesFunction();
        private delegate ValueTask<Wss> ReturningWssFunction();

        private IQueryable<Wss> TryCatch(ReturningWssesFunction returningRmosFunction)
        {
            try
            {
                return returningRmosFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedWssStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedWssServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private WssDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new WssDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private WssServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new WssServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }
    }
}
