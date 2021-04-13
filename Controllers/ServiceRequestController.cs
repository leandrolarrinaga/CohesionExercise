using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using System.Web.Http;

namespace exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        public static readonly List<Models.ServiceRequest> serviceRequests = new()
        {
            //For testing exercise, in memory
            new Models.ServiceRequest { id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"), buildingCode = "AAAAA", currentStatus = Models.CurrentStatus.Complete, description = "First", createdBy = "Leandro R Larrinaga", createdDate = DateTime.Now, lastModifiedBy = "Leandro R Larrinaga", lastModifiedDate = DateTime.Now }
        };

        [HttpGet]
        public IActionResult Get()
        {
            if (serviceRequests.Count == 0)
                return NoContent();

            return Ok(serviceRequests);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var serviceRequest = serviceRequests.FirstOrDefault(x => x.id == id);

            if (serviceRequest == null)
                return NotFound();

            return Ok(serviceRequest);
        }

        [HttpPost]
        public IActionResult Post(Models.PostRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var serviceRequest = new Models.ServiceRequest
            {
                id = Guid.NewGuid(),
                buildingCode = body.buildingCode,
                description = body.description,
                createdDate = DateTime.Now,
                lastModifiedDate = DateTime.Now
                //TODO: Get user and update createdBy
            };

            serviceRequests.Add(serviceRequest);

            return Created(Request.Path.Value, serviceRequest.id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Models.PutRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var serviceRequest = serviceRequests.FirstOrDefault(x => x.id == id);

            if (serviceRequest == null)
                return NotFound();

            if (!String.IsNullOrWhiteSpace(body.buildingCode))
                serviceRequest.buildingCode = body.buildingCode;

            if (!String.IsNullOrWhiteSpace(body.description))
                serviceRequest.description = body.description;

            if (body.currentStatus != null)
                serviceRequest.currentStatus = (Models.CurrentStatus)body.currentStatus; //This should be checked before cast

            serviceRequest.lastModifiedDate = DateTime.Now;
            //TODO: Get user and update modifiedBy
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var serviceRequest = serviceRequests.FirstOrDefault(x => x.id == id);

            if (serviceRequest == null)
                return NotFound();

            var result = serviceRequests.RemoveAll(x => x.id == id);

            return Ok();
        }
    }
}
