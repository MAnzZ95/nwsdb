//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.Provinces.Exceptions;
using Nwsdb.Web.Api.Models.Provinces;
using Nwsdb.Web.Api.Services.Foundations.Provinces;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.RMOs;
using Nwsdb.Web.Api.Models.RMOs;
using Nwsdb.Web.Api.Models.RMOs.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RmosController : RESTFulController
    {
        private readonly IRmoService rmoService;

        public RmosController(IRmoService rmoService)
        {
            this.rmoService = rmoService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Rmo>>> GetAllOwnerShips()
        {
            try
            {
                IQueryable<Rmo> storageRmos =
                    this.rmoService.RetrieveAllRmos();
                return Ok(storageRmos);
            }
            catch (RmoDependencyException rmoDependencyException)
            {
                return InternalServerError(rmoDependencyException);
            }
            catch (RmoServiceException rmoServiceException)
            {
                return InternalServerError(rmoServiceException);
            }
        }
    }
}
