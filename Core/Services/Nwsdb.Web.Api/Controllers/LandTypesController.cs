//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.UserTypes.Exceptions;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.LandTypes;
using Nwsdb.Web.Api.Models.LandTypes;
using Nwsdb.Web.Api.Models.LandTypes.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandTypesController : RESTFulController
    {
        private readonly ILandTypeService landTypeService;

        public LandTypesController(ILandTypeService landTypeService)
        {
            this.landTypeService = landTypeService;
        }


        [HttpPost]
        public async ValueTask<ActionResult<LandType>> PostLandTypeAsync(LandType landType)
        {
            try
            {
                var addedLandType = await this.landTypeService.AddLandTypeAsync(landType);

                return Ok(addedLandType);
            }
            catch (UserTypeValidationException userTypeValidationException)
            {
                return BadRequest(userTypeValidationException.InnerException);
            }
            catch (UserTypeDependencyException userTypeDependencyException)
            {
                return InternalServerError(userTypeDependencyException);
            }
            catch (UserTypeServiceException userTypeServiceException)
            {
                return InternalServerError(userTypeServiceException);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<LandType>> GetLandTypeById(Guid id)
        {
            try
            {
                var retrivedLandType = await this.landTypeService.RetrieveLandTypeById(id);
                return Ok(retrivedLandType);
            }
            catch (LandTypeValidationException landTypeValidationException)
                when (landTypeValidationException.InnerException is NotFoundLandTypeException)
            {
                return NotFound(landTypeValidationException.InnerException);
            }
            catch (LandTypeValidationException landTypeValidationException)
            {
                return NotFound(landTypeValidationException.InnerException);
            }
            catch (LandTypeDependencyException landTypeDependencyException)
            {
                return InternalServerError(landTypeDependencyException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<LandType>>> GetAllLandTypes()
        {
            try
            {
                IQueryable<LandType> storageLandTypes =
                    this.landTypeService.RetrieveAllLandTypes();
                return Ok(storageLandTypes);
            }
            catch (LandTypeDependencyException landTypeDependencyException)
            {
                return InternalServerError(landTypeDependencyException);
            }
            catch (LandTypeServiceException landTypeServiceException)
            {
                return InternalServerError(landTypeServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<LandType>> PutLandTypeTypeAsync(LandType userType)
        {
            try
            {
                LandType modifiedLandType =s
                    await this.landTypeService.ModifyLandTypeAsync(userType);
                return Ok(modifiedLandType);
            }
            catch (LandTypeValidationException landTypeValidationException)
                when (landTypeValidationException.InnerException is NotFoundLandTypeException)
            {
                return NotFound(landTypeValidationException.InnerException);
            }
            catch (LandTypeValidationException landTypeValidationException)
            {
                return BadRequest(landTypeValidationException.InnerException);
            }
            catch (LandTypeServiceException landTypeServiceException)
            {
                return InternalServerError(landTypeServiceException);
            }
        }

    }
}
