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
using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Services.Foundations.Lands;
using Nwsdb.Web.Api.Models.WSSs;
using Nwsdb.Web.Api.Models.RSCs;

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
        public async ValueTask<ActionResult<IQueryable<Rmo>>> GetAllRmos()
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

        [HttpGet("count")]
        public ActionResult GetRmosCount()
        {
            try
            {
                int storageRmoCount = rmoService.RetrieveAllRmos().Count();
                return Ok(storageRmoCount);
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

        [HttpGet("{rmoId}/{rscId}")]
        public async ValueTask<ActionResult<IQueryable<Rmo>>> GetAllRmosByrmoIdAndRscId(Guid rmoId, Guid rscId)
        {
            try
            {
                IQueryable<Rmo> storageRmos =
                    this.rmoService.RetreveAllRmosByrmoIdAndRscId(rmoId, rscId);

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

        [HttpGet("{rscId}")]
        public async ValueTask<ActionResult<IQueryable<Rmo>>> GetAllRmosByRscId(Guid rscId)
        {
            try
            {
                IQueryable<Rmo> storageRmos =
                    this.rmoService.RetreveAllRmosByRscId(rscId);
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

        [HttpPut]
        public async ValueTask<ActionResult<Rmo>> PutRmoAsync(Rmo rmo)
        {
            try
            {
                Rmo modifiedRmo =
                    await this.rmoService.ModifyRmoAsync(rmo);
                return Ok(modifiedRmo);
            }
            catch (DistrictValidationException companyValidationException)
                when (companyValidationException.InnerException is NotFoundLandException)
            {
                return NotFound(companyValidationException.InnerException);
            }
            catch (DistrictValidationException companyValidationException)
            {
                return BadRequest(companyValidationException.InnerException);
            }
            catch (LandServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }

        [HttpPost]
        public async ValueTask<ActionResult<Rmo>> PostRmoAsync(Rmo rmo)
        {
            try
            {
                var addedRmo = await this.rmoService.AddRmoAsync(rmo);

                return Ok(addedRmo);
            }
            catch (DistrictValidationException landValidationException)
            {
                return BadRequest(landValidationException.InnerException);
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
