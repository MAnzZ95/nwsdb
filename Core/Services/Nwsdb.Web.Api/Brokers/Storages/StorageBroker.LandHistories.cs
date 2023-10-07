//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.LandHistories;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<LandHistory> LandHistories { get; set; }

        public async ValueTask<LandHistory> InserLandHistoryAsync(LandHistory landHistories) =>
            await InsertAsync(landHistories);

        public IQueryable<LandHistory> SelectAllLandHistoryHistories() =>
            this.LandHistories;

        public async ValueTask<LandHistory> UpdateLandHistoryAsync(LandHistory landHistories) =>
            await UpdateAsync(landHistories);

        public async ValueTask<LandHistory> SelectLandHistoryById(Guid id) =>
            await SelectAsync<LandHistory>(id);
    }
}
