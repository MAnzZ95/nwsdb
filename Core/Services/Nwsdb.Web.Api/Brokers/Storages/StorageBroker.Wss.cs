//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.Users;
using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Wss> Wsses { get; set; }

        public async ValueTask<Wss> InsertWssAsync(Wss wss) =>
            await InsertAsync(wss);

        public IQueryable<Wss> SelectAllWsses() =>
            this.Wsses.Include(wss => wss.Rmo);

        public async ValueTask<Wss> UpdateWssAsync(Wss wss) =>
            await UpdateAsync(wss);

        public async ValueTask<Wss> SelectWssById(Guid id) =>
            await SelectAsync<Wss>(id);
    }
}
