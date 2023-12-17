//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.DSDevisions;
using Nwsdb.Web.Api.Models.GSDivisions;
using Nwsdb.Web.Api.Models.GSDivisions.Exceptions;
using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Services.Foundations.DSDivisions;
using Nwsdb.Web.Api.Services.Foundations.GSDivisions;
using RESTFulSense.Controllers;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GSDivisionsController : RESTFulController
    {
        private readonly IGSDivisionService gSDivisionService;

        public GSDivisionsController(IGSDivisionService gSDivisionService)
        {
            this.gSDivisionService = gSDivisionService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<GSDivision>>> GetAllGSDivisions()
        {
            try
            {
                IQueryable<GSDivision> storageLands =
                    this.gSDivisionService.RetrieveAllGSDivisions();

                return Ok(storageLands);
            }
            catch (GSDivisionDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (GSDivisionServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }


        [HttpGet("{dsDivisionId}")]
        public async ValueTask<ActionResult<IQueryable<GSDivision>>> GetAllGSDivisionsByDsDivisionId(Guid dsDivisionId)
        {
            try
            {
                IQueryable<GSDivision> storageGSDivisions =
                    this.gSDivisionService.RetrieveAllGSDivisionsByDsDivisionId(dsDivisionId);

                return Ok(storageGSDivisions);
            }
            catch (GSDivisionDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (GSDivisionServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }
    }
}
