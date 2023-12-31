﻿//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.DSDevisions;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<DSDivision> DSDivisions { get; set; }

        public async ValueTask<DSDivision> InserDSDivisionAsync(DSDivision dsDivision) =>
            await InsertAsync(dsDivision);

        public IQueryable<DSDivision> SelectAllDSDivisions() =>
            this.DSDivisions.Include(dsd => dsd.District);

        public async ValueTask<DSDivision> UpdateDSDivisionAsync(DSDivision dsDivision) =>
            await UpdateAsync(dsDivision);

        public async ValueTask<DSDivision> SelectDSDivisionById(Guid id) =>
            await SelectAsync<DSDivision>(id);
    }
}
