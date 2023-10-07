//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.RMOs;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Rmo> InsertRmoAsync(Rmo rmo);
        IQueryable<Rmo> SelectAllRmos();
        ValueTask<Rmo> UpdateRmoAsync(Rmo rmo);
        ValueTask<Rmo> SelectRmoById(Guid id);
    }
}
