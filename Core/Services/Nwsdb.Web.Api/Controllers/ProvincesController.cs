//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.OwnerShips.Exceptions;
using Nwsdb.Web.Api.Models.OwnerShips;
using Nwsdb.Web.Api.Services.Foundations.OwnerShips;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.Provinces;
using Nwsdb.Web.Api.Models.Provinces;
using Nwsdb.Web.Api.Models.Provinces.Exceptions;
using Microsoft.AspNetCore.OData.Query;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : RESTFulController
    {
        private readonly IProvinceService provinceService;

        public ProvincesController(IProvinceService provinceService)
        {
            this.provinceService = provinceService;
        }

        [HttpGet]
        [EnableQuery]
        public async ValueTask<ActionResult<IQueryable<Province>>> GetAllOwnerShips()
        {
            try
            {
                IQueryable<Province> storageProvinces =
                    this.provinceService.RetrieveAllProvinces();
                return Ok(storageProvinces);
            }
            catch (ProvinceDependencyException provinceDependencyException)
            {
                return InternalServerError(provinceDependencyException);
            }
            catch (ProvinceServiceException provinceServiceException)
            {
                return InternalServerError(provinceServiceException);
            }
        }
    }
}
