//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using Sheenam.Api.Services.Processings.HomeRequests;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeRequestsController : RESTFulController
    {
        private readonly IHomeRequestProcessingService homeRequestProcessingService;

        public HomeRequestsController(IHomeRequestProcessingService homeRequestProcessingService)
        {
            this.homeRequestProcessingService = homeRequestProcessingService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<HomeRequest>> PostHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                HomeRequest postedHomeRequest = await this.homeRequestProcessingService.RegisterAndSaveHomeRequestAsync(homeRequest);

                return Created(postedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
                when (homeRequestDependencyValidationException.InnerException is AlreadyExistHomeRequestException)
            {
                return Conflict(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpGet("ById")]
        public async ValueTask<ActionResult<HomeRequest>> GetHomeRequestByIdAsync(Guid homeRequestId)
        {
            try
            {
                return await this.homeRequestProcessingService.RetrieveHomeRequestByIdAsync(homeRequestId);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
                when (homeRequestValidationException.InnerException is InvalidHomeRequestException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
                when (homeRequestValidationException.InnerException is NotFoundHomeRequestException)
            {
                return NotFound(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpGet("All")]
        public ActionResult<IQueryable<HomeRequest>> GetAllHomeRequests()
        {
            try
            {
                IQueryable<HomeRequest> allHomeRequests = this.homeRequestProcessingService.RetrieveAllHomeRequests();

                return Ok(allHomeRequests);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<HomeRequest>> PutHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                HomeRequest modifyHomeRequest =
                    await this.homeRequestProcessingService.ModifyHomeRequestAsync(homeRequest);

                return Ok(modifyHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
                when (homeRequestValidationException.InnerException is NotFoundHomeRequestException)
            {
                return NotFound(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return Conflict(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpDelete]
        public async ValueTask<ActionResult<HomeRequest>> DeleteHomeRequestAsync(Guid homeRequestId)
        {
            try
            {
                HomeRequest deleteHomeRequest = await this.homeRequestProcessingService.RemoveHomeRequestByIdAsync(homeRequestId);

                return Ok(deleteHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
                when (homeRequestValidationException.InnerException is NotFoundHomeRequestException)
            {
                return NotFound(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
                when (homeRequestDependencyValidationException.InnerException is LockedHomeRequestException)
            {
                return Locked(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }
    }
}
