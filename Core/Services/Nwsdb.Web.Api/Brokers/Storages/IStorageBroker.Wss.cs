//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Wss> InsertWssAsync(Wss wss);
        IQueryable<Wss> SelectAllWsses();
        ValueTask<Wss> UpdateWssAsync(Wss wss);
        ValueTask<Wss> SelectWssById(Guid id);
    }
}
