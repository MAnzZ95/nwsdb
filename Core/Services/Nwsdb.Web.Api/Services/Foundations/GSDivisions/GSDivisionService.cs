//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Models.DSDevisions;
using Nwsdb.Web.Api.Models.GSDivisions;

namespace Nwsdb.Web.Api.Services.Foundations.GSDivisions
{
    public partial class GSDivisionService : IGSDivisionService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public GSDivisionService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public IQueryable<GSDivision> RetrieveAllGSDivisions() =>
            TryCatch(() =>
                this.storageBroker.SelectAllGSDivisions());

        public IQueryable<GSDivision> RetrieveAllGSDivisionsByDsDivisionId(Guid dsDivisionId)
        {
            IQueryable<GSDivision> gSDivisions = this.storageBroker.SelectAllGSDivisions();
            gSDivisions =  gSDivisions.Where(gsd => gsd.DSDivisionId == dsDivisionId);

            return gSDivisions;
        }
    }
}
