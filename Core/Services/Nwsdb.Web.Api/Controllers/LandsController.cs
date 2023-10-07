//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.Lands;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.Lands.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandsController : RESTFulController
    {
        private readonly ILandService landService;

        public LandsController(ILandService landService)
        {
            this.landService = landService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Land>> PostUserAsync(Land land)
        {
            try
            {
                var addedLand = await this.landService.AddLandAsync(land);

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
        public async ValueTask<ActionResult<Land>> GetLandById(Guid id)
        {
            try
            {
                var retrivedLand = await this.landService.RetrieveLandById(id);
                return Ok(retrivedLand);
            }
            catch(LandHistoryValidationException landValidationException) 
                when(landValidationException.InnerException is NotFoundLandHistoryException)
            {
                return NotFound(landValidationException.InnerException);
            }
            catch(LandHistoryValidationException landValidationException)
            {
                return NotFound(landValidationException.InnerException);
            }
            catch(LandHistoryDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Land>>> GetAllLands()
        {
            try
            {
                IQueryable<Land> storageLands =
                    this.landService.RetrieveAllLands();
                return Ok(storageLands);
            }
            catch(LandHistoryDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch(LandHistoryServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Land>> PutLandAsync(Land land)
        {
            try
            {
                Land modifiedLand =
                    await this.landService.ModifyLandAsync(land);
                return Ok(modifiedLand);
            }
            catch(LandHistoryValidationException companyValidationException)
                when(companyValidationException.InnerException is NotFoundLandHistoryException)
            {
                return NotFound(companyValidationException.InnerException);
            }
            catch(LandHistoryValidationException companyValidationException)
            {
                return BadRequest(companyValidationException.InnerException);
            }
            catch(LandHistoryServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }

    }
}
