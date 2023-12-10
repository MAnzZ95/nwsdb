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
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Models.OwnerShips;
using Nwsdb.Web.Api.Models.OwnerShips.Exceptions;
using Nwsdb.Web.Api.Services.Foundations.Users;
using Nwsdb.Web.Api.Services.Foundations.OwnerShips;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerShipsController : RESTFulController
    {
        private readonly IOwnerShipService ownerShipService;

        public OwnerShipsController(IOwnerShipService ownerShipService)
        {
            this.ownerShipService = ownerShipService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<OwnerShip>>> GetAllOwnerShips()
        {
            try
            {
                IQueryable<OwnerShip> storageOwnerShips =
                    this.ownerShipService.RetrieveAllOwnerShips();
                return Ok(storageOwnerShips);
            }
            catch (OwnerShipDependencyException ownerShipDependencyException)
            {
                return InternalServerError(ownerShipDependencyException);
            }
            catch (OwnerShipServiceException ownerShipServiceException)
            {
                return InternalServerError(ownerShipServiceException);
            }
        }

    }
}
