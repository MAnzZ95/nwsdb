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
using Nwsdb.Web.Api.Models.RMOs;
using Nwsdb.Web.Api.Models.RSCs;

namespace Nwsdb.Web.Api.Services.Foundations.RSCs
{
    public partial class RscService : IRscService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public RscService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public IQueryable<Rsc> RetrieveAllRscs() =>
            TryCatch(() =>
                this.storageBroker.SelectAllRscs());

        public async ValueTask<Rsc> RetriveRscById(Guid id)
        {
            return await this.storageBroker.SelectRscById(id);
        }

        public async ValueTask<Rsc> ModifyRscAsync(Rsc rsc)
        {
            Rsc maybeRsc = await storageBroker.SelectRscById(rsc.Id);
            rsc.UpdatedDate = DateTime.UtcNow;
            rsc.CreatedBy = maybeRsc.CreatedBy;
            rsc.CreatedDate = maybeRsc.CreatedDate;
            rsc.UpdatedBy = Guid.Parse("67fe9d83-18db-4b45-bf2e-e56a3f996a81");

            return await this.storageBroker.UpdateRscAsync(rsc);
        }

        public ValueTask<Rsc> AddRscAsync(Rsc rsc)
        {
            rsc.Id = Guid.NewGuid();
            rsc.CreatedDate = DateTime.UtcNow;
            rsc.UpdatedDate = DateTime.UtcNow;
            rsc.CreatedBy = Guid.Parse("67fe9d83-18db-4b45-bf2e-e56a3f996a81");
            rsc.UpdatedBy = Guid.Parse("67fe9d83-18db-4b45-bf2e-e56a3f996a81");

            return this.storageBroker.InsertRscAsync(rsc);
        }
        
        public async ValueTask<Rsc> RemoveRscById(Guid id)
        {
            Rsc maybeRsc = await this.storageBroker.SelectRscById(id);

            if(maybeRsc != null)
            {
                maybeRsc.Status = RscStatus.Removed;
                maybeRsc.UpdatedDate = DateTimeOffset.UtcNow;                
            }

            return await this.storageBroker.UpdateRscAsync(maybeRsc);
        }
    }
}
