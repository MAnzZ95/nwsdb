//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Nwsdb.Web.Api.Models.UserTypes;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<UserType> UserTypes { get; set; }

        public async ValueTask<UserType> InserUserTypeAsync(UserType userType) =>
            await InsertAsync(userType);

        public IQueryable<UserType> SelectAllUserTypes() =>
            this.UserTypes;

        public async ValueTask<UserType> UpdateUserTypeAsync(UserType user) =>
            await UpdateAsync(user);

        public async ValueTask<UserType> SelectUserTypeById(Guid id) =>
            await SelectAsync<UserType>(id);
    }
}
