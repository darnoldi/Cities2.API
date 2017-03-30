using Cities2.API;
using Cities2.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities2.API.Controllers
{   
    [Route("api/cities")]

    public class PointsOf_InterestController : Controller
    {
       [HttpGet("{cityId}/pointsofinterest")]

       public IActionResult GetPontsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }



        [HttpGet("{cityId}/pointsofinterest/{Id}", Name = "GetPointOfInterest")]

        public IActionResult GetPontOfInterest(int cityId, int Id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            
            if (city == null)
            {
                return NotFound();
            }


            var pointofinterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == Id);

            if (pointofinterest == null)
            {
                return NotFound();
            }


            return Ok(pointofinterest);
        }


        [HttpPost("{cityId}/pointsofinterest")]

        public IActionResult CreatePointOfInterest(int cityId,  [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
           if (pointOfInterest == null)
            {
                return BadRequest();
            }

           if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", " Field: 'Name' And Field: 'Description' cannot contain the same value.");
            }

           if (!ModelState.IsValid)
                {
                return BadRequest();
                }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // this needs to be improved
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                            c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointsOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, Id = finalPointOfInterest.Id }, finalPointOfInterest);

        }

        [HttpPatch("{cityId}/pointsofinterest/{Id}")]

        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int Id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc
            )
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointofinterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == Id);

            if (pointofinterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointofinterestFromStore.Name,
                Description = pointofinterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", " Field: 'Name' And Field: 'Description' cannot contain the same value.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointofinterestFromStore.Name = pointOfInterestToPatch.Name;
            pointofinterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }


    }
}
