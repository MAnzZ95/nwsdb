﻿//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.UserTypes;

namespace Nwsdb.Web.Api.Services.Foundations.UserTypes
{
    public interface IUserTypeService
    {
        ValueTask<UserType> AddUserTypeAsync(UserType userType);
        IQueryable<UserType> RetrieveAllUserTypes();
        ValueTask<UserType> RetrieveUserTypeById(Guid id);
        ValueTask<UserType> ModifyUserTypeAsync(UserType userType);
    }
}
