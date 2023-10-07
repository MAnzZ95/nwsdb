//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.RSCs;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Rsc> Rscs { get; set; }

        public async ValueTask<Rsc> InsertRscAsync(Rsc rsc) =>
            await InsertAsync(rsc);

        public IQueryable<Rsc> SelectAllRscs() =>
            this.Rscs;

        public async ValueTask<Rsc> UpdateRscAsync(Rsc rsc) =>
            await UpdateAsync(rsc);

        public async ValueTask<Rsc> SelectRscById(Guid id) =>
            await SelectAsync<Rsc>(id);
    }
}
