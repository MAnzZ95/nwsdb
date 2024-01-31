//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Nwsdb.Web.Api.Models.RMOs.Exceptions;
using Nwsdb.Web.Api.Models.Users;
using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Services.Foundations.RMOs;
using Nwsdb.Web.Api.Services.Foundations.Users;
using RESTFulSense.Controllers;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<User>> PostUserAsync(User user)
        {
            try
            {
                var addedUser = await this.userService.AddUserAsync(user);

                return Ok(addedUser);
            }
            catch(UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch(UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch(UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<User>> GetLandById(Guid id)
        {
            try
            {
                var retrivedUser = await this.userService.RetrieveUserById(id);
                return Ok(retrivedUser);
            }
            catch (UserValidationException userValidationException)
                when (userValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userValidationException.InnerException);
            }
            catch (UserValidationException userValidationException)
            {
                return NotFound(userValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
        }

        [HttpGet]
        [EnableQuery]  
        public async ValueTask<ActionResult<IQueryable<User>>> GetAllUsers()
        {
            try
            {
                IQueryable<User> storageUsers =
                    this.userService.RetrieveAllUsers();
                return Ok(storageUsers);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<User>> PutUserAsync(User user)
        {
            try
            {
                User modifiedUser =
                    await this.userService.ModifyUserAsync(user);
                return Ok(modifiedUser);
            }
            catch (UserValidationException userValidationException)
                when (userValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userValidationException.InnerException);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }

        [HttpGet("count")]
        public ActionResult GetUsersCount()
        {
            try
            {
                int storageUsersCount = userService.RetrieveAllUsers().Count();
                return Ok(storageUsersCount);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }
    }
}
