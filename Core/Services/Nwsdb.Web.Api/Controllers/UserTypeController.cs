//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Models.Users;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Services.Foundations.Users;
using Nwsdb.Web.Api.Services.Foundations.UserTypes;
using Nwsdb.Web.Api.Models.UserTypes;
using Nwsdb.Web.Api.Models.UserTypes.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : RESTFulController
    {
        private readonly IUserTypeService userTypeService;

        public UserTypeController(IUserTypeService userTypeService)
        {
            this.userTypeService = userTypeService;
        }


        [HttpPost]
        public async ValueTask<ActionResult<UserType>> PostUserAsync(UserType userType)
        {
            try
            {
                var addedUserType = await this.userTypeService.AddUserTypeAsync(userType);

                return Ok(addedUserType);
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
    }
}
