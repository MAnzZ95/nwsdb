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
using Nwsdb.Web.Api.Services.Foundations.Wsses;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Models.WSSs;
using Nwsdb.Web.Api.Models.WSSs.Exceptions;
using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Services.Foundations.Users;
using Nwsdb.Web.Api.Models.RMOs.Exceptions;
using Nwsdb.Web.Api.Models.RMOs;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WssesController : RESTFulController
    {
        private readonly IWssService wssService;

        public WssesController(IWssService WssService)
        {
            this.wssService = WssService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Wss>>> GetAllWsses()
        {
            try
            {
                IQueryable<Wss> storageLands =
                    this.wssService.RetrieveAllWsses();
                return Ok(storageLands);
            }
            catch (WssDependencyException WssDependencyException)
            {
                return InternalServerError(WssDependencyException);
            }
            catch (WssServiceException WssServiceException)
            {
                return InternalServerError(WssServiceException);
            }
        }

        [HttpGet("count")]
        public ActionResult GetWssesCount()
        {
            try
            {
                int storagewssesCount = wssService.RetrieveAllWsses().Count();
                return Ok(storagewssesCount);
            }
            catch (WssDependencyException wssDependencyException)
            {
                return InternalServerError(wssDependencyException);
            }
            catch (WssServiceException wssServiceException)
            {
                return InternalServerError(wssServiceException);
            }
        }

        [HttpGet("{wssId}/{rmoId}")]
        public async ValueTask<ActionResult<IQueryable<Wss>>> GetAllWssesByWssIdAndRmoId(Guid wssId, Guid rmoId )
        {
            try
            {
                IQueryable<Wss> storageWsses =
                    this.wssService.RetreveAllWssesByWssIdAndRmoId(wssId,rmoId);

                return Ok(storageWsses);
            }
            catch (WssDependencyException WssDependencyException)
            {
                return InternalServerError(WssDependencyException);
            }
            catch (WssServiceException WssServiceException)
            {
                return InternalServerError(WssServiceException);
            }
        }

        [HttpGet("{rmoId}")]
        public async ValueTask<ActionResult<IQueryable<Wss>>> GetAllWssesByRmoId(Guid rmoId)
        {
            try
            {
                IQueryable<Wss> storageWsses =
                    this.wssService.RetreveAllWssesByRmoId( rmoId);

                return Ok(storageWsses);
            }
            catch (WssDependencyException WssDependencyException)
            {
                return InternalServerError(WssDependencyException);
            }
            catch (WssServiceException WssServiceException)
            {
                return InternalServerError(WssServiceException);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<Wss>> GetWssById(Guid id)
        {
            try
            {
                Wss retrivedWss = await this.wssService.RetriveWssById(id);

                return Ok(retrivedWss);
            }
            catch (DistrictValidationException landValidationException)
                when (landValidationException.InnerException is NotFoundLandException)
            {
                return NotFound(landValidationException.InnerException);
            }
            catch (DistrictValidationException landValidationException)
            {
                return NotFound(landValidationException.InnerException);
            }
            catch (LandDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Wss>> PutRmoAsync(Wss wss)
        {
            try
            {
                Wss modifiedWss =
                    await this.wssService.ModifyWssAsync(wss);

                return Ok(modifiedWss);
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
        public async ValueTask<ActionResult<Wss>> PostRmoAsync(Wss wss)
        {
            try
            {
                var addedWss = await this.wssService.AddWssAsync(wss);

                return Ok(addedWss);
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
