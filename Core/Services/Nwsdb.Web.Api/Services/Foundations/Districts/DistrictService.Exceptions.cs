//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Npgsql;
using Nwsdb.Web.Api.Models.Districts;
using Nwsdb.Web.Api.Models.Districts.Exceptions;
using Xeptions;

namespace Nwsdb.Web.Api.Services.Foundations.Districts
{
    public partial class DistrictService
    {
        private delegate IQueryable<District> ReturningDistrictsFunction();

        private IQueryable<District> TryCatch(ReturningDistrictsFunction returningDistrictsFunction)
        {
            try
            {
                return returningDistrictsFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedDistrictStorageException = new FailedDistrictStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedDistrictStorageException);
            }
            catch (Exception exception)
            {
                var failedDistrictServiceException = new FailedDistrictServiceException(exception);

                throw CreateAndLogServiceException(failedDistrictServiceException);
            }
        }

        private DistrictDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var districtDependencyException = new DistrictDependencyException(exception);
            this.loggingBroker.LogCritical(districtDependencyException);

            throw districtDependencyException;
        }

        private DistrictServiceException CreateAndLogServiceException(Xeption exception)
        {
            var districtServiceException = new DistrictServiceException(exception);
            this.loggingBroker.LogError(districtServiceException);

            throw districtServiceException;
        }
    }
}
