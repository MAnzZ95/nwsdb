﻿//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Services.Foundations.Wsses
{
    public interface IWssService
    {
        IQueryable<Wss> RetrieveAllWsses();
        IQueryable<Wss> RetreveAllWssesByWssIdAndRmoId(Guid wssId, Guid rmoId);
        IQueryable<Wss> RetreveAllWssesByRmoId(Guid rmoId);
        ValueTask<Wss> RemoveWssById(Guid id);
        ValueTask<Wss> AddWssAsync(Wss wss);
        ValueTask<Wss> ModifyWssAsync(Wss wss);
        ValueTask<Wss> RetriveWssById(Guid id);
    }
}
