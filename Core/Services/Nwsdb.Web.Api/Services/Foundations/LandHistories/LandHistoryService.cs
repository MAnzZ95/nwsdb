//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Models.LandHistories;

namespace Nwsdb.Web.Api.Services.Foundations.LandHistories
{
    public partial class LandHistoryService : ILandHistoryService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public LandHistoryService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<LandHistory> AddLandHistoryAsync(LandHistory landHistory) =>
            TryCatch(async () =>
            {
                ValidateLandHistoryOnAdd(landHistory);

                return await this.storageBroker.InserLandHistoryAsync(landHistory);
            });

        public IQueryable<LandHistory> RetrieveAllLands() =>
            TryCatch(() =>
                this.storageBroker.SelectAllLandHistoryHistories());

        public ValueTask<LandHistory> RetrieveLandHistoryById(Guid id) =>
            TryCatch(async () =>
            {
                ValidateLandHistoryId(id);

                return await this.storageBroker.SelectLandHistoryById(id);
            });

        public ValueTask<LandHistory> ModifyLandAsync(LandHistory land) =>
            TryCatch(async () =>
            {
                ValidateLandHistoryOnAdd(land);

                return await this.storageBroker.UpdateLandHistoryAsync(land);
            });
    }
}
