using Npgsql;
using Nwsdb.Web.Api.Models.Provinces.Exceptions;
using Nwsdb.Web.Api.Models.Provinces;
using Xeptions;
using Nwsdb.Web.Api.Models.RMOs;
using Nwsdb.Web.Api.Models.RMOs.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.RMOs
{
    public partial class RmoService
    {
        private delegate IQueryable<Rmo> ReturningRmosFunction();
        private delegate ValueTask<Rmo> ReturningRmoFunction();

        private IQueryable<Rmo> TryCatch(ReturningRmosFunction returningRmosFunction)
        {
            try
            {
                return returningRmosFunction();
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedLandStorageException = new FailedRmoStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedLandStorageException);
            }
            catch (Exception exception)
            {
                var failedLandServiceException = new FailedRmoServiceException(exception);

                throw CreateAndLogServiceException(failedLandServiceException);
            }
        }

        private RmoDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var landDependencyException = new RmoDependencyException(exception);
            this.loggingBroker.LogCritical(landDependencyException);

            throw landDependencyException;
        }

        private RmoServiceException CreateAndLogServiceException(Xeption exception)
        {
            var landServiceException = new RmoServiceException(exception);
            this.loggingBroker.LogError(landServiceException);

            throw landServiceException;
        }
    }
}
