//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.DSDevisions;

namespace Nwsdb.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<DSDivision> InserDSDivisionAsync(DSDivision dsDivision);
        IQueryable<DSDivision> SelectAllDSDivisions();
        ValueTask<DSDivision> UpdateDSDivisionAsync(DSDivision dsDivision);
        ValueTask<DSDivision> SelectDSDivisionById(Guid id);
    }
}
