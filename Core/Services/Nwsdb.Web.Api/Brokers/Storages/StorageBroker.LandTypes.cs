//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.LandTypes;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<LandType> LandTypes { get; set; }

        public async ValueTask<LandType> InserLandTypeAsync(LandType landTypes) =>
            await InsertAsync(landTypes);

        public IQueryable<LandType> SelectAllLandTypes() =>
            this.LandTypes;

        public async ValueTask<LandType> UpdateLandTypeAsync(LandType landTypes) =>
            await UpdateAsync(landTypes);

        public async ValueTask<LandType> SelectLandTypeById(Guid id) =>
            await SelectAsync<LandType>(id);
    }
}
