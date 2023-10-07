//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.Districts;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<District> Districts { get; set; }

        public async ValueTask<District> InserDistrictAsync(District district) =>
            await InsertAsync(district);

        public IQueryable<District> SelectAllDistrics() =>
            this.Districts;

        public async ValueTask<District> UpdateDistrictAsync(District district) =>
            await UpdateAsync(district);

        public async ValueTask<District> SelectDistrictById(Guid id) =>
            await SelectAsync<District>(id);
    }
}
