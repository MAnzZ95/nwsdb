//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Models.Provinces;
using Nwsdb.Web.Api.Models.RMOs;
using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Services.Foundations.RMOs
{
    public partial class RmoService : IRmoService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public RmoService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public IQueryable<Rmo> RetrieveAllRmos() =>
            TryCatch(() =>
                this.storageBroker.SelectAllRmos());

        public IQueryable<Rmo> RetreveAllRmosByrmoIdAndRscId(Guid rmoId, Guid rscId)
        {
            IQueryable<Rmo> rmos = this.storageBroker.SelectAllRmos();

            rmos = rmos.Where(rmo => rmo.Id == rmoId && rmo.RscId == rscId);

            return rmos;
        }

        public IQueryable<Rmo> RetreveAllRmosByRscId(Guid rscId)
        {
            IQueryable<Rmo> rmos = this.storageBroker.SelectAllRmos();

            rmos = rmos.Where(rmo =>  rmo.RscId == rscId);

            return rmos;
        }

    }
}
