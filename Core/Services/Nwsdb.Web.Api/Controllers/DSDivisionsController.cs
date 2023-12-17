//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.DSDivisions;
using Nwsdb.Web.Api.Models.DSDevisions;
using Nwsdb.Web.Api.Models.DSDivisions.Exceptions;

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
                IQueryable<DSDivision> storageDSDivisions =
                    this.dSDivisionService.RetrieveAllDSDivisions();

                return Ok(storageDSDivisions);
            }
            catch (DSDivisionDependencyException dSDivisionDependencyException)
            {
                return InternalServerError(dSDivisionDependencyException);
            }
            catch (DSDivisionServiceException dSDivisionServiceException)
            {
                return InternalServerError(dSDivisionServiceException);
            }
        }

        [HttpGet("{districtId}")]
        public async ValueTask<ActionResult<IQueryable<DSDivision>>> GetAllDSDivisionsByDistrictId(Guid districtId)
        {
            try
            {
                IQueryable<DSDivision> storageDSDivisions =
                    this.dSDivisionService.RetrieveAllDSDivisionsByDistrictId(districtId);

                return Ok(storageDSDivisions);
            }
            catch (DSDivisionDependencyException dSDivisionDependencyException)
            {
                return InternalServerError(dSDivisionDependencyException);
            }
            catch (DSDivisionServiceException dSDivisionServiceException)
            {
                return InternalServerError(dSDivisionServiceException);
            }
        }
    }
}
