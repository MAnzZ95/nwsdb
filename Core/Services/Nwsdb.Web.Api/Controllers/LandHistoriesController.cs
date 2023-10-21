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
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.LandHistories;
using Nwsdb.Web.Api.Models.LandHistories;
using Nwsdb.Web.Api.Models.LandHistories.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandHistoriesController : RESTFulController
    {
        private readonly ILandHistoryService landHistoryService;

        public LandHistoriesController(ILandHistoryService landHistoryService)
        {
            this.landHistoryService = landHistoryService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<LandHistory>> PostUserAsync(LandHistory landHistory)
        {
            try
            {
                var addedLand = await this.landHistoryService.AddLandHistoryAsync(landHistory);

                return Ok(addedLand);
            }
            catch (LandHistoryValidationException landValidationException)
            {
                return BadRequest(landValidationException.InnerException);
            }
            catch (LandHistoryDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (LandHistoryServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<LandHistory>> GetLandHistoryById(Guid id)
        {
            try
            {
                var retrivedLand = await this.landHistoryService.RetrieveLandHistoryById(id);
                return Ok(retrivedLand);
            }
            catch (LandHistoryValidationException landValidationException)
                when (landValidationException.InnerException is NotFoundLandHistoryException)
            {
                return NotFound(landValidationException.InnerException);
            }
            catch (LandHistoryValidationException landValidationException)
            {
                return NotFound(landValidationException.InnerException);
            }
            catch (LandHistoryDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<LandHistory>>> GetAllLandHistories()
        {
            try
            {
                IQueryable<LandHistory> storageLands =
                    this.landHistoryService.RetrieveAllLandHistories();
                return Ok(storageLands);
            }
            catch (LandHistoryDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (LandHistoryServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<LandHistory>> PutLandHistoryAsync(LandHistory land)
        {
            try
            {
                LandHistory modifiedLand =
                    await this.landHistoryService.ModifyLandHistoryAsync(land);
                return Ok(modifiedLand);
            }
            catch (LandHistoryValidationException companyValidationException)
                when (companyValidationException.InnerException is NotFoundLandHistoryException)
            {
                return NotFound(companyValidationException.InnerException);
            }
            catch (LandHistoryValidationException companyValidationException)
            {
                return BadRequest(companyValidationException.InnerException);
            }
            catch (LandHistoryServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }
    }
}
