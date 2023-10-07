//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Services.Foundations.Lands;
using Nwsdb.Web.Api.Services.Foundations.Wsses;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Models.WSSs;
using Nwsdb.Web.Api.Models.WSSs.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WssesController : RESTFulController
    {
        private readonly IWssService WssService;

        public WssesController(IWssService WssService)
        {
            this.WssService = WssService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Wss>>> GetAllWsses()
        {
            try
            {
                IQueryable<Wss> storageLands =
                    this.WssService.RetrieveAllWsses();
                return Ok(storageLands);
            }
            catch (WssDependencyException WssDependencyException)
            {
                return InternalServerError(WssDependencyException);
            }
            catch (WssServiceException WssServiceException)
            {
                return InternalServerError(WssServiceException);
            }
        }
    }
}
