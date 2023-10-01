//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Models.Lands;

namespace Nwsdb.Web.Api.Services.Foundations.Lands
{
    public partial class LandService : ILandService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public LandService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<Land> AddLandAsync(Land land) =>
            TryCatch(async () =>
            {
                ValidateLandOnAdd(land);

                return await this.storageBroker.InserLandAsync(land);
            });

        public IQueryable<Land> RetrieveAllLands() =>
            TryCatch(() =>
                this.storageBroker.SelectAllLands());

        public ValueTask<Land> RetrieveLandById(Guid id) =>
            TryCatch(async () =>
            {
                ValidateLandId(id);

                return await this.storageBroker.SelectLandById(id);
            });

        public ValueTask<Land> ModifyLandAsync(Land land) =>
            TryCatch(async () =>
            {
                ValidateLandOnAdd(land);

                return await this.storageBroker.UpdateLandAsync(land);
            });
    }
}
