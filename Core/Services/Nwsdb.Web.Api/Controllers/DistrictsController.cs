using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Services.Foundations.Districts;
using Nwsdb.Web.Api.Services.Foundations.Lands;
using RESTFulSense.Controllers;
using Nwsdb.Web.Api.Models.Districts;
using Nwsdb.Web.Api.Models.Districts.Exceptions;

namespace Nwsdb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : RESTFulController
    {
        private readonly IDistrictService districtService;

        public DistrictsController(IDistrictService districtService)
        {
            this.districtService = districtService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<District>>> GetDistricts()
        {
            try
            {
                var retrivedLand = this.districtService.RetrieveAllDistricts();
                return Ok(retrivedLand);
            }
            catch (DistrictDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (DistrictServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }

        [HttpGet("{provinceId}")]
        public async ValueTask<ActionResult<IQueryable<District>>> GetDistrictsByProvinceId(Guid provinceId)
        {
            try
            {
                var retrivedDistricts = this.districtService.RetrieveAllDistrictsByProvinceId(provinceId);
                return Ok(retrivedDistricts);
            }
            catch (DistrictDependencyException landDependencyException)
            {
                return InternalServerError(landDependencyException);
            }
            catch (DistrictServiceException landServiceException)
            {
                return InternalServerError(landServiceException);
            }
        }
    }
}
