//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Sheenam.Api.Services.Processings.Hosts;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostsController : RESTFulController
    {
        private readonly IHostProcessingService hostProcessingService;

        public HostsController(IHostProcessingService hostProcessingService)
        {
            this.hostProcessingService = hostProcessingService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Host>> PostHostAsync(Host host)
        {
            try
            {
                Host postedHost = await this.hostProcessingService.RegisterAndSaveHostAsync(host);

                return Created(postedHost);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
                when (hostDependencyValidationException.InnerException is AlreadyExistHostException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
            {
                return BadRequest(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return InternalServerError(hostServiceException.InnerException);
            }
        }

        [HttpGet("ById")]
        public async ValueTask<ActionResult<Host>> GetHostByIdAsync(Guid hostId)
        {
            try
            {
                return await this.hostProcessingService.RetrieveHostByIdAsync(hostId);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostValidationException hostValidationException)
                when (hostValidationException.InnerException is InvalidHostException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostValidationException hostValidationException)
                when (hostValidationException.InnerException is NotFoundHostException)
            {
                return NotFound(hostValidationException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return InternalServerError(hostServiceException.InnerException);
            }
        }

        [HttpGet("All")]
        public ActionResult<IQueryable<Host>> GetAllHosts()
        {
            try
            {
                IQueryable<Host> allHosts = this.hostProcessingService.RetrieveAllHosts();

                return Ok(allHosts);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return InternalServerError(hostServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Host>> PutHostAsync(Host host)
        {
            try
            {
                Host modifyHost =
                    await this.hostProcessingService.ModifyHostAsync(host);

                return Ok(modifyHost);
            }
            catch (HostValidationException hostValidationException)
                when (hostValidationException.InnerException is NotFoundHostException)
            {
                return NotFound(hostValidationException.InnerException);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return InternalServerError(hostServiceException.InnerException);
            }
        }

        [HttpDelete]
        public async ValueTask<ActionResult<Host>> DeleteHostAsync(Guid hostId)
        {
            try
            {
                Host deleteHost = await this.hostProcessingService.RemoveHostByIdAsync(hostId);

                return Ok(deleteHost);
            }
            catch (HostValidationException hostValidationException)
                when (hostValidationException.InnerException is NotFoundHostException)
            {
                return NotFound(hostValidationException.InnerException);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
                when (hostDependencyValidationException.InnerException is LockedHostException)
            {
                return Locked(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
            {
                return BadRequest(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return InternalServerError(hostServiceException.InnerException);
            }
        }
    }
}
