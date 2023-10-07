//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.OwnerShips;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<OwnerShip> OwnerShips { get; set; }

        public async ValueTask<OwnerShip> InsertOwnerShipAsync(OwnerShip ownerShip) =>
            await InsertAsync(ownerShip);

        public IQueryable<OwnerShip> SelectAllOwnerShips() =>
            this.OwnerShips;

        public async ValueTask<OwnerShip> UpdateOwnerShipAsync(OwnerShip ownerShip) =>
            await UpdateAsync(ownerShip);

        public async ValueTask<OwnerShip> SelectOwnerShipById(Guid id) =>
            await SelectAsync<OwnerShip>(id);
    }
}
