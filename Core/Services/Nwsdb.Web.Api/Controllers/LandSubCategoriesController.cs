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
using Nwsdb.Web.Api.Services.Foundations.LandSubCategories;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Models.LandSubCategories;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandSubCategoriesController : RESTFulController
    {
        private readonly ILandSubCategoryService landSubCategoryService;

        public LandSubCategoriesController(ILandSubCategoryService landSubCategoryService)
        {
            this.landSubCategoryService = landSubCategoryService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<LandSubCategory>>> GetAllLandSubCategories()
        {
            try
            {
                IQueryable<LandSubCategory> storageLands =
                    this.landSubCategoryService.RetrieveAllLandSubCategories();
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
