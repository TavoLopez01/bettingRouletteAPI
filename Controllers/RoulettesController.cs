using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers;
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
        private readonly NewRouletteResult _newRouletteResult;
        private readonly GlobalFunctions _globalFunctions;
        public RoulettesController(RoulettesModel rouleteModel)
        {
            _rouletteModel = rouleteModel;
            _newRouletteResult = new NewRouletteResult();
            _globalFunctions = new GlobalFunctions();
        }
        [HttpGet]
        public IActionResult GetRoulette()
        {
            return Ok(_rouletteModel.GetRoulette());
        }
        [HttpPost]
        public IActionResult CreateRoulette(Roulette roulette)
        {
            roulette.Status = "Close";
            roulette.CreatedAt = _globalFunctions.GetDateFromFormat("o");
            _rouletteModel.CreateRoulette(roulette);
            _newRouletteResult.IdRoulette = roulette.Id.ToString();

            return Ok(_newRouletteResult);
        }
    }
}
