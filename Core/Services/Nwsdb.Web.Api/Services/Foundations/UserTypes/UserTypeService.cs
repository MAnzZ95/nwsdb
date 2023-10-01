//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Models.UserTypes;

namespace Nwsdb.Web.Api.Services.Foundations.UserTypes
{
    public partial class UserTypeService : IUserTypeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public UserTypeService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<UserType> AddUserTypeAsync(UserType userType) =>
            TryCatch(async () =>
            {
                ValidateUserTypeOnAdd(userType);

                return await this.storageBroker.InserUserTypeAsync(userType);
            });

        public IQueryable<UserType> RetrieveAllUserTypes() =>
            TryCatch(() =>
                this.storageBroker.SelectAllUserTypes()).Where(userType => userType.Status != UserTypeStatus.Removed);

        public ValueTask<UserType> RetrieveUserTypeById(Guid id) =>
            TryCatch(async () =>
            {
                ValidateUserTypeId(id);

                return await this.storageBroker.SelectUserTypeById(id);
            });

        public ValueTask<UserType> ModifyUserTypeAsync(UserType userType) =>
            TryCatch(async () =>
            {
                ValidateUserTypeOnAdd(userType);

                return await this.storageBroker.UpdateUserTypeAsync(userType);
            });
    }
}
