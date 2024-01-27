//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.RSCs;

namespace Nwsdb.Web.Api.Services.Foundations.RSCs
{
    public interface IRscService
    {
        IQueryable<Rsc> RetrieveAllRscs();
        ValueTask<Rsc> ModifyRscAsync(Rsc rsc);
        ValueTask<Rsc> RetriveRscById(Guid id);
        ValueTask<Rsc> AddRscAsync(Rsc rsc);
        ValueTask<Rsc> RemoveRscById(Guid id);
    }
}
