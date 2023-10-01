//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.Users;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Land> Lands { get; set; }

        public async ValueTask<Land> InserLandAsync(Land land) =>
            await InsertAsync(land);

        public IQueryable<Land> SelectAllLands() =>
            this.Lands;

        public async ValueTask<Land> UpdateLandAsync(Land land) =>
            await UpdateAsync(land);

        public async ValueTask<Land> SelectLandById(Guid id) =>
            await SelectAsync<Land>(id);
    }
}
