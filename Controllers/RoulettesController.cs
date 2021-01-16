using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bettingRouletteAPI.Controllers
{
    [Route("api/roulettes")]
    [ApiController]
    public class RoulettesController : ControllerBase
    {

        private readonly RoulettesModel _rouletteModel;

        public RoulettesController(RoulettesModel rouleteModel)
        {
            _rouletteModel = rouleteModel;
        }

        [HttpGet]
        public IActionResult GetRoulette()
        {
            return Ok(_rouletteModel.GetRoulette());
        }

        
        [HttpGet("{id:length(24)}", Name = "GetRoulette")]
        public IActionResult GetRouletteById(string idRoulette)
        {
            var roulette = _rouletteModel.GetRouletteById(idRoulette);
            if (roulette != null) {
                return NotFound();
            }

            return Ok(roulette);
        }

        [HttpPost]
        public IActionResult CreateRoulete(Roulette roulette)
        {
            var newRouletteResult = new NewRouletteResult();
            newRouletteResult.IdRoulette = roulette.Id.ToString();
            roulette.Status = "Close";
            _rouletteModel.CreateRoulette(roulette);

            return Ok(newRouletteResult);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteRouletteById(string idRoulette)
        {
            var roulette = _rouletteModel.GetRouletteById(idRoulette);
            if (roulette == null)
            {
                return NotFound();
            }
            _rouletteModel.DeleteRouletteById(roulette.Id);

            return NoContent();
        }

    }
}
