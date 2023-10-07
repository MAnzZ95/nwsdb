//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.OwnerShips;

namespace Nwsdb.Web.Api.Services.Foundations.OwnerShips
{
    public interface IOwnerShipService
    {
        IQueryable<OwnerShip> RetrieveAllOwnerShips();
    }
}
