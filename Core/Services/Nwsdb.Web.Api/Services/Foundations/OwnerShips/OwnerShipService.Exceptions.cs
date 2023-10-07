//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.LandTypes.Exceptions;
using Nwsdb.Web.Api.Models.LandTypes;
using Xeptions;
using Nwsdb.Web.Api.Models.OwnerShips;
using Nwsdb.Web.Api.Models.OwnerShips.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.OwnerShips
{
    public partial class OwnerShipService
    {
        private delegate IQueryable<OwnerShip> ReturningOwnerShipsFunction();
        private delegate ValueTask<OwnerShip> ReturningOwnerShipeFunction();

        private IQueryable<OwnerShip> TryCatch(ReturningOwnerShipsFunction returningLandTypesFunction)
        {
            try
            {
                return returningLandTypesFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedOwnerShipStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedOwnerShipServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private OwnerShipDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new OwnerShipDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private OwnerShipServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new OwnerShipServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }
    }
}
