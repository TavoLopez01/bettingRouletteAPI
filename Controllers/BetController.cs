using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Helpers.Validations;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace bettingRouletteAPI.Controllers
{
    [Route("api/createBet")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly BetsModel _betsModel;
        private readonly TokensModel _tokensModel;
        private readonly RoulettesModel _roulettesModel;
        private readonly Validations _validations;
        private readonly SuccessMessageResult _successMessageResult;
        private readonly ErrorMessageResult _errorMessageResult;

        public BetController(BetsModel betsModel, TokensModel tokensModel, RoulettesModel roulettesModel)
        {
            _betsModel = betsModel;
            _tokensModel = tokensModel;
            _roulettesModel = roulettesModel;
            _validations = new Validations();
            _successMessageResult = new SuccessMessageResult();
            _errorMessageResult = new ErrorMessageResult();
        }

        [HttpPost]
        public IActionResult CreateBet(Bet bet)
        {
            var request = Request;
            var result = _validations.ValidateIfTokenExist(request, _tokensModel);
            if (result == "Ok")
            {
                var resultValidateRoulette = _validations.ValidateIfExistRouletteOpen(bet, _roulettesModel);
                if (resultValidateRoulette.Status)
                {
                    var resultValidateBet = _validations.ValidateBet(bet, _roulettesModel);
                    if (resultValidateBet.Status)
                    {
                        _betsModel.CreateBet(bet);
                        _successMessageResult.Success = "Bet created correctly";

                        return Ok(_successMessageResult);
                    } else
                    {
                        _errorMessageResult.Error = resultValidateBet.Message;

                        return Ok(_errorMessageResult);
                    }
                } else
                {
                    _errorMessageResult.Error = resultValidateRoulette.Message;
                 
                    return Ok(_errorMessageResult);
                }
            }
            _errorMessageResult.Error = "Authentication header is required";

            return Ok(_errorMessageResult);
        }
    }
}
