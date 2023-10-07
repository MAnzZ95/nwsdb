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
using Nwsdb.Web.Api.Services.Foundations.Lands;
using Nwsdb.Web.Api.Services.Foundations.DSDivisions;
using Nwsdb.Web.Api.Models.DSDevisions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DSDivisionsController : RESTFulController
    {
        private readonly IDSDivisionService dSDivisionService;

        public DSDivisionsController(IDSDivisionService dSDivisionService)
        {
            this.dSDivisionService = dSDivisionService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<DSDivision>>> GetAllDSDivisions()
        {
            try
            {
                IQueryable<DSDivision> storageLands =
                    this.dSDivisionService.RetrieveAllDSDivisions();
                return Ok(storageLands);
            }
            catch (LandDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (LandServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }
    }
}
