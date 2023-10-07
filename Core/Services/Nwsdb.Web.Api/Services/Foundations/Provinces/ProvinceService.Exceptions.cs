//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.OwnerShips.Exceptions;
using Nwsdb.Web.Api.Models.OwnerShips;
using Xeptions;
using Nwsdb.Web.Api.Models.Provinces;
using Nwsdb.Web.Api.Models.Provinces.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.Provinces
{
    public partial class ProvinceService
    {
        private delegate IQueryable<Province> ReturningProvincesFunction();
        private delegate ValueTask<Province> ReturningProvinceFunction();

        private IQueryable<Province> TryCatch(ReturningProvincesFunction returningLandTypesFunction)
        {
            try
            {
                return returningLandTypesFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedProvinceStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedProvinceServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private ProvinceDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new ProvinceDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private ProvinceServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new ProvinceServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }
    }
}
