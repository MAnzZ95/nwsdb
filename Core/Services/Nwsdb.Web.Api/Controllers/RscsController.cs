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
using Nwsdb.Web.Api.Services.Foundations.RSCs;
using Nwsdb.Web.Api.Models.RSCs;
using Nwsdb.Web.Api.Models.RSCs.Exceptions;
using Nwsdb.Web.Api.Models.RMOs.Exceptions;
using Nwsdb.Web.Api.Services.Foundations.RMOs;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RscsController : RESTFulController
    {
        private readonly IRscService rscService;

        public RscsController(IRscService rscService)
        {
            this.rscService = rscService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Rsc>>> GetAllRscs()
        {
            try
            {
                IQueryable<Rsc> storageRscs =
                    this.rscService.RetrieveAllRscs();
                return Ok(storageRscs);
            }
            catch (RscDependencyException rscDependencyException)
            {
                return InternalServerError(rscDependencyException);
            }
            catch (RscServiceException rscServiceException)
            {
                return InternalServerError(rscServiceException);
            }
        }

        [HttpGet("count")]
        public ActionResult GetRscsCount()
        {
            try
            {
                int storageRscCount = rscService.RetrieveAllRscs().Count();
                return Ok(storageRscCount);
            }
            catch (RscDependencyException rscDependencyException)
            {
                return InternalServerError(rscDependencyException);
            }
            catch (RscServiceException rscServiceException)
            {
                return InternalServerError(rscServiceException);
            }
        }
    }
}
