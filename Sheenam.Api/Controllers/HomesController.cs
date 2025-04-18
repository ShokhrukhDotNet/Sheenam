//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using Sheenam.Api.Services.Processings.Homes;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomesController : RESTFulController
    {
        private readonly IHomeProcessingService homeProcessingService;

        public HomesController(IHomeProcessingService homeProcessingService)
        {
            this.homeProcessingService = homeProcessingService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Home>> PostHomeAsync(Home home)
        {
            try
            {
                Home postedHome = await this.homeProcessingService.RegisterAndSaveHomeAsync(home);

                return Created(postedHome);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
                when (homeDependencyValidationException.InnerException is AlreadyExistHomeException)
            {
                return Conflict(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
            {
                return BadRequest(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

        [HttpGet("ById")]
        public async ValueTask<ActionResult<Home>> GetHomeByIdAsync(Guid homeId)
        {
            try
            {
                return await this.homeProcessingService.RetrieveHomeByIdAsync(homeId);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is InvalidHomeException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is NotFoundHomeException)
            {
                return NotFound(homeValidationException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

        [HttpGet("All")]
        public ActionResult<IQueryable<Home>> GetAllHomes()
        {
            try
            {
                IQueryable<Home> allHomes = this.homeProcessingService.RetrieveAllHomes();

                return Ok(allHomes);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Home>> PutHomeAsync(Home home)
        {
            try
            {
                Home modifyHome =
                    await this.homeProcessingService.ModifyHomeAsync(home);

                return Ok(modifyHome);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is NotFoundHomeException)
            {
                return NotFound(homeValidationException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
            {
                return Conflict(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

        [HttpDelete]
        public async ValueTask<ActionResult<Home>> DeleteHomeAsync(Guid homeId)
        {
            try
            {
                Home deleteHome = await this.homeProcessingService.RemoveHomeByIdAsync(homeId);

                return Ok(deleteHome);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is NotFoundHomeException)
            {
                return NotFound(homeValidationException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
                when (homeDependencyValidationException.InnerException is LockedHomeException)
            {
                return Locked(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
            {
                return BadRequest(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }
    }
}
