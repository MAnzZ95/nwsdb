//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.OwnerShips;
using Nwsdb.Web.Api.Models.RMOs;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Rmo> Rmos { get; set; }

        public async ValueTask<Rmo> InsertRmoAsync(Rmo rmo) =>
            await InsertAsync(rmo);

        public IQueryable<Rmo> SelectAllRmos() =>
            this.Rmos.Include(rmo => rmo.Rsc);

        public async ValueTask<Rmo> UpdateRmoAsync(Rmo rmo) =>
            await UpdateAsync(rmo);

        public async ValueTask<Rmo> SelectRmoById(Guid id) =>
            await SelectAsync<Rmo>(id);
    }
}
