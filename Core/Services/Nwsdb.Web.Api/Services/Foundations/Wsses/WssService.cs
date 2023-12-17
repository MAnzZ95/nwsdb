//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
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
    }
}
