using bettingRouletteAPI.Helpers;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Mvc;
namespace bettingRouletteAPI.Controllers
{
    [Route("api/openRoulette")]
    [ApiController]
    public class OpenRouletteController : ControllerBase
    {
        private readonly RoulettesModel _rouletteModel;
        private readonly GlobalFunctions _globalFunctions;
        public OpenRouletteController(RoulettesModel rouleteModel)
        {
            _rouletteModel = rouleteModel;
            _globalFunctions = new GlobalFunctions();
        }
        [HttpPut("{idRoulette}")]
        public IActionResult UpdateRoulette(string idRoulette)
        {
            var errorMessageResult = new ErrorMessageResult();
            errorMessageResult.Error = "Operation rejected";
            if (idRoulette.Length == 24)
            {
                var roulette = _rouletteModel.GetRouletteById(idRoulette);
                if (roulette == null)
                {
                    return Ok(errorMessageResult);
                }
                roulette.Status = "Open";
                roulette.OpenedDate = _globalFunctions.GetDateFromFormat("o");
                _rouletteModel.UpdateRoulette(idRoulette, roulette);
                var successMessageResult = new SuccessMessageResult();
                successMessageResult.Success = "Successful operation";

                return Ok(successMessageResult);
            } else
            {
                return Ok(errorMessageResult);
            }
        }
    }
}
