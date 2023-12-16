//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.GSDivisions;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<GSDivision> GSDivisions { get; set; }

        public async ValueTask<GSDivision> InserGSDivisionAsync(GSDivision gsDivision) =>
            await InsertAsync(gsDivision);

        public IQueryable<GSDivision> SelectAllGSDivisions() =>
            this.GSDivisions.Include(gnd => gnd.DSDivision);

        public async ValueTask<GSDivision> UpdateGSDivisionAsync(GSDivision gsDivision) =>
            await UpdateAsync(gsDivision);

        public async ValueTask<GSDivision> SelectGSDivisionById(Guid id) =>
            await SelectAsync<GSDivision>(id);
    }
}
