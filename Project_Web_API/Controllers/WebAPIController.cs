using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Project_Web_API.Data;
using Project_Web_API.Models;
using Project_Web_API.Models.DTO;

namespace Project_Web_API.Controllers
{
    [Route("api/WebAPI")]
    [ApiController]
    public class WebAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
            
        }

        [HttpGet("id", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest("The Id parameter is no found");
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla(VillaDTO villaDTO)
        {
            if(villaDTO == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if(VillaStore.VillaList.FirstOrDefault(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
                {
                ModelState.AddModelError("", "The Name must be unique");
                return BadRequest(ModelState);
            }

            villaDTO.Id = VillaStore.VillaList.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;

            VillaStore.VillaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new {Id =  villaDTO.Id}, villaDTO);

        }


        [HttpDelete("id")]
        public ActionResult <VillaDTO> DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest("The Id must be greater than 0");
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if(villa == null)
            {
                return NotFound("The villa is not Found");
            }

            VillaStore.VillaList.Remove(villa);

            return NoContent();

        }

        [HttpPut("id")]

        public ActionResult<VillaDTO> UpdateVilla(int id, VillaDTO villaDTO)
        {
            if(id == 0 || id != villaDTO.Id) {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id==id);

            villa.Id = villaDTO.Id;
            villa.Name = villaDTO.Name;

            return NoContent();
        }


        [HttpPatch("id")]
        public ActionResult<VillaDTO> PartialUpdate(int id, JsonPatchDocument<VillaDTO> patch)

        {

            if (patch == null || id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if (villa != null)
            { 
            patch.ApplyTo(villa);
        }

            return NoContent();



        }
    }
}
