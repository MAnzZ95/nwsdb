//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Models.RMOs;
using Nwsdb.Web.Api.Models.RSCs;
using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Services.Foundations.Wsses
{
    public partial class WssService : IWssService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public WssService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public IQueryable<Wss> RetrieveAllWsses() =>
            TryCatch(() =>
                this.storageBroker.SelectAllWsses());

        public IQueryable<Wss> RetreveAllWssesByWssIdAndRmoId(Guid wssId, Guid rmoId)
        {
            IQueryable<Wss> wsses = this.storageBroker.SelectAllWsses();

            wsses = wsses.Where(wss => wss.Id == wssId && wss.RmoId == rmoId);

            return wsses;
        }

        public IQueryable<Wss> RetreveAllWssesByRmoId(Guid rmoId)
        {
            IQueryable<Wss> wsses = this.storageBroker.SelectAllWsses();

            wsses = wsses.Where(wss => wss.RmoId == rmoId);

            return wsses;
        }

        public async ValueTask<Wss> RetriveWssById(Guid id)
        {
            return await this.storageBroker.SelectWssById(id);
        }

        public async ValueTask<Wss> ModifyWssAsync(Wss wss)
        {
            Wss maybeWss = await storageBroker.SelectWssById(wss.Id);
            wss.UpdatedDate = DateTime.UtcNow;
            wss.CreatedBy = maybeWss.CreatedBy;
            wss.CreatedDate = maybeWss.CreatedDate;
            wss.UpdatedBy = Guid.Parse("67fe9d83-18db-4b45-bf2e-e56a3f996a81");

            return await this.storageBroker.UpdateWssAsync(wss);
        }

        public async ValueTask<Wss> AddWssAsync(Wss wss)
        {
            wss.Id = Guid.NewGuid();
            wss.CreatedDate = DateTime.UtcNow;
            wss.UpdatedDate = DateTime.UtcNow;
            wss.CreatedBy = Guid.Parse("67fe9d83-18db-4b45-bf2e-e56a3f996a81");
            wss.UpdatedBy = Guid.Parse("67fe9d83-18db-4b45-bf2e-e56a3f996a81");

            return await this.storageBroker.InsertWssAsync(wss);
        }

        public async ValueTask<Wss> RemoveWssById(Guid id)
        {
            Wss maybeWss = await this.storageBroker.SelectWssById(id);

            if (maybeWss != null)
            {
                maybeWss.Status = WssStatus.Removed;
                maybeWss.UpdatedDate = DateTimeOffset.UtcNow;
            }

            return await this.storageBroker.UpdateWssAsync(maybeWss);
        }
    }
}
