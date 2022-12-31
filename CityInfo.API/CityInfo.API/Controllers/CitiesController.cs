using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        // [HttpGet("api/cities")] /// آدرس این اکشن به صورت مقابل تعریف شده است

        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> GetCities()
        {
            //var result = new JsonResult(CitiesDataStore.Current.Cities);
            //result.StatusCode= 200;

            // return new JsonResult(CitiesDataStore.Current.Cities);
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{cityId}")]
        public ActionResult<CityDTO> GetCity(int cityId)
        {
            //return new JsonResult(CitiesDataStore.Current.Cities.
            //    FirstOrDefault(c => c.CityID == cityId));

            var cityToReturn = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.CityID == cityId);

            if(cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}
