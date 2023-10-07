//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.OwnerShips;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<OwnerShip> InsertOwnerShipAsync(OwnerShip ownerShip);
        IQueryable<OwnerShip> SelectAllOwnerShips();
        ValueTask<OwnerShip> UpdateOwnerShipAsync(OwnerShip ownerShip);
        ValueTask<OwnerShip> SelectOwnerShipById(Guid id);
    }
}
