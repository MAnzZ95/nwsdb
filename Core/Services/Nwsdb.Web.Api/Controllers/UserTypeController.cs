//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
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

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<UserType>> GetLandById(Guid id)
        {
            try
            {
                var retrivedUserType = await this.userTypeService.RetrieveUserTypeById(id);
                return Ok(retrivedUserType);
            }
            catch (UserTypeValidationException userTypeValidationException)
                when (userTypeValidationException.InnerException is NotFoundUserTypeException)
            {
                return NotFound(userTypeValidationException.InnerException);
            }
            catch (UserTypeValidationException userTypeValidationException)
            {
                return NotFound(userTypeValidationException.InnerException);
            }
            catch (UserTypeDependencyException userTypeDependencyException)
            {
                return InternalServerError(userTypeDependencyException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<UserType>>> GetAllUsers()
        {
            try
            {
                IQueryable<UserType> storageUserTypes =
                    this.userTypeService.RetrieveAllUserTypes();
                return Ok(storageUserTypes);
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

        [HttpPut]
        public async ValueTask<ActionResult<UserType>> PutUserTypeAsync(UserType userType)
        {
            try
            {
                UserType modifiedUserType =
                    await this.userTypeService.ModifyUserTypeAsync(userType);
                return Ok(modifiedUserType);
            }
            catch (UserTypeValidationException userTypeValidationException)
                when (userTypeValidationException.InnerException is NotFoundUserTypeException)
            {
                return NotFound(userTypeValidationException.InnerException);
            }
            catch (UserTypeValidationException userTypeValidationException)
            {
                return BadRequest(userTypeValidationException.InnerException);
            }
            catch (UserTypeServiceException userTypeServiceException)
            {
                return InternalServerError(userTypeServiceException);
            }
        }
    }
}
