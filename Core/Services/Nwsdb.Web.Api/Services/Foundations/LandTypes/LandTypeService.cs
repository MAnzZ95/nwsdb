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
using Nwsdb.Web.Api.Models.LandTypes;
using Nwsdb.Web.Api.Models.Users;

namespace Nwsdb.Web.Api.Services.Foundations.LandTypes
{
    public partial class LandTypeService : ILandTypeService
    {

        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public LandTypeService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public IQueryable<LandType> RetrieveAllLandTypes() =>
            TryCatch(() =>
                this.storageBroker.SelectAllLandTypes());

        public ValueTask<LandType> AddLandTypeAsync(LandType landType) =>
           TryCatch(async () =>
           {
               ValidateLandTypeOnAdd(landType);

               return await this.storageBroker.InserLandTypeAsync(landType);
           });

        public ValueTask<LandType> RetrieveLandTypeById(Guid id) =>
            TryCatch(async () =>
            {
                ValidateLandTypeId(id);

                return await this.storageBroker.SelectLandTypeById(id);
            });

        public ValueTask<LandType> ModifyLandTypeAsync(LandType landType) =>
            TryCatch(async () =>
            {
                ValidateLandTypeOnAdd(landType);

                return await this.storageBroker.UpdateLandTypeAsync(landType);
            });
    }
}
