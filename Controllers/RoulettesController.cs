using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult CreateRoulete(Roulette roulette)
        {
            var newRouletteResult = new NewRouletteResult();
            newRouletteResult.IdRoulette = roulette.Id.ToString();
            roulette.Status = "Close";
            _rouletteModel.CreateRoulette(roulette);

            return Ok(newRouletteResult);
        }

    }
}
