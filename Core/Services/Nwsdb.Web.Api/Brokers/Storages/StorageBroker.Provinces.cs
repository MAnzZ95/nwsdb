//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.Provinces;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Province> Provinces { get; set; }

        public async ValueTask<Province> InserProvinceAsync(Province province) =>
            await InsertAsync(province);

        public IQueryable<Province> SelectAllProvinces() =>
            this.Provinces;

        public async ValueTask<Province> UpdateProvinceAsync(Province province) =>
            await UpdateAsync(province);

        public async ValueTask<Province> SelectProvinceById(Guid id) =>
            await SelectAsync<Province>(id);
    }
}
