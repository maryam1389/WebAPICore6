using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    //api/cities/1/pointOfInterest

    [Route("api/cities/{cityId}/pointOfInterest")]
    [ApiController]
    public class PointsOfInterestController : Controller
    {
        #region Logger
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly LocalMailService _localMailService;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            LocalMailService localMailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
        }
        #endregion


        #region GetAll Point
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointsOfInterst(int cityId)
        {
            try
            {
               // throw new Exception("Exseption sample ...");

                var city = CitiesDataStore.Current.Cities
               .FirstOrDefault(c => c.CityID == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with {cityId} wasnt found"); // لاگ زن در برنامه
                    return NotFound();
                }

                return Ok(city.PointOfInterests);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption getting {cityId}",ex);
                return StatusCode(500,"A problem happened while ....");
            }         

        }
        #endregion

        #region GetOne Point Of interese
        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDTO> GetPointOfInterest
           (int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.CityID == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var point = city.PointOfInterests
                .FirstOrDefault(p => p.PointID == pointOfInterestId);
            if (point == null)
            {
                return NotFound();
            }
            return Ok(point);

        }
        #endregion

        #region Create
        [HttpPost]
        public ActionResult CreatePointOfIntereste(
        int cityId, [FromBody] PointOfInterestForCreationDTO pointOfInterest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities
               .FirstOrDefault(c => c.CityID == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxpointOfInterestID = CitiesDataStore.Current.Cities
                .SelectMany(c => c.PointOfInterests).Max(p => p.PointID);


            var creatpoint = new PointOfInterestDTO()
            {
                PointID = +maxpointOfInterestID,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            city.PointOfInterests.Add(creatpoint);

            return CreatedAtAction("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = creatpoint.PointID,
                }, creatpoint);

        }
        #endregion

        #region Update
        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId,
            int pointOfInterestId,
            PointOfInterestForUpdateDTO pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //find city
            var city = CitiesDataStore.Current.Cities
               .FirstOrDefault(c => c.CityID == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // find point of interest 
            var point = city.PointOfInterests
                .FirstOrDefault(p => p.PointID == pointOfInterestId);
            if (point == null)
            {
                return NotFound();
            }

            //update point of interest
            point.Name = pointOfInterest.Name;
            point.Description = pointOfInterest.Description;

            return NoContent();
        }

        #endregion

        #region Patch
        [HttpPatch("{poitOfInteresId}")]
        public ActionResult PartialyUpdatePointOfInterest(int cityId, int poitOfInteresId,
          JsonPatchDocument<PointOfInterestForUpdateDTO> patchDocument)
        {
            //find city
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.CityID == cityId);
            if (city == null)
                return NotFound();

            // find Point Of City
            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(p => p.PointID == poitOfInteresId);
            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInteresToPatch = new PointOfInterestForUpdateDTO()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description,
            };

            patchDocument.ApplyTo(pointOfInteresToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(pointOfInteresToPatch))
            {
                return BadRequest(modelState: ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInteresToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInteresToPatch.Description;

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(
        int cityId,
        int pointOfInterestId)
        {
            //find city
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.CityID == cityId);
            if (city == null)
                return NotFound();


            //find point of city
            var point = city.PointOfInterests
                .FirstOrDefault(p => p.PointID == pointOfInterestId);
            if (point == null)
                return NotFound();

            city.PointOfInterests.Remove(point);

            _localMailService
                .send(
                "Point Of Interest Deleted",
                $"point of interest {point.Name} with id {point.PointID}");

            return NoContent();
        }
        #endregion



    }
}
