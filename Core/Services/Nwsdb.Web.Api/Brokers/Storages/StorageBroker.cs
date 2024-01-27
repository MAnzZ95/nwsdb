//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;
        private IHttpContextAccessor httpContextAccessor;

        public StorageBroker(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionHost = configuration["DBPostgreSQL:ConnectionString:Host"];
            var connectionPort = configuration["DBPostgreSQL:ConnectionString:Port"];
            var connectionDatabase = configuration["DBPostgreSQL:ConnectionString:Database"];
            var connectionUsername = configuration["DBPostgreSQL:ConnectionString:Username"];
            var connectionPassword = configuration["DBPostgreSQL:ConnectionString:Password"];

            var connectionString = $"Host={connectionHost}:{connectionPort};Database={connectionDatabase}" +
                $";Username={connectionUsername};Password='{connectionPassword}'";

            optionsBuilder.UseNpgsql(connectionString);
        }

        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration, this.httpContextAccessor);
            broker.Entry(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();
            return @object;
        }

        private async ValueTask<T> SelectAsync<T>(params object[] objectIds) where T : class =>
            await FindAsync<T>(objectIds);

        private async ValueTask<T> SelectAsync<T, TRelatedEntity>(object ObjectIds, params object[] paramSet)
            where T : class
            where TRelatedEntity : class
        {
            var entity = await FindAsync<T>(ObjectIds);
            if (entity == null)
            {
                foreach(string param in paramSet)
                {
                    await Entry(entity).Reference(param).LoadAsync();
                }
            }

            return entity;
        }

        private async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration,this.httpContextAccessor);
            broker.Entry(@object).State = EntityState.Modified;
            await broker.SaveChangesAsync();
            return @object;
        }

        //public override Task<int> SaveChangesAsync()
        
    }
}
