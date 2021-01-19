using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace bettingRouletteAPI.Controllers
{
    [Route("api/closeBets")]
    [ApiController]
    public class CloseBetsController : ControllerBase
    {
        private readonly RoulettesModel _rouletteModel;
        private readonly BetsModel _betModel;
        private readonly GlobalFunctions _globalFunctions;
        private readonly List<WinningBet> _listWinningBet;
        private readonly SuccessCloseBetsResult _successCloseBetsResult;
        private readonly ErrorMessageResult _errorMessageResult;
        public CloseBetsController(RoulettesModel rouletteModel, BetsModel betModel)
        {
            _rouletteModel = rouletteModel;
            _betModel = betModel;
            _globalFunctions = new GlobalFunctions();
            _listWinningBet = new List<WinningBet>();
            _successCloseBetsResult = new SuccessCloseBetsResult();
            _errorMessageResult = new ErrorMessageResult();
        }
        [HttpPut("{idRoulette}")]
        public IActionResult CloseBets(string idRoulette)
        {
            if (idRoulette.Length == 24)
            {
                var roulette = _rouletteModel.GetRouletteById(idRoulette);
                if (FindRouletteAndCloseIfExist(roulette))
                {
                    var listBets = _betModel.GetListBetsByIdRoulette(_rouletteModel.GetRouletteById(idRoulette));
                    FillListOfWinningBets(listBets);

                    return Ok(_successCloseBetsResult);
                } else
                {
                    _errorMessageResult.Error = "The Id_Roulette is not valid";
                    return Ok(_errorMessageResult);
                }
            }
            else
            {
                _errorMessageResult.Error = "The Id_Roulette is not valid";

                return Ok(_errorMessageResult);
            }
        }
        public bool FindRouletteAndCloseIfExist(Roulette roulette)
        {
            var result = _rouletteModel.GetRouletteById(roulette.Id);
            if (result != null)
            {
                result.Status = "Close";
                result.CloseDate = _globalFunctions.GetDateFromFormat("o");
                _rouletteModel.UpdateRoulette(roulette.Id, result);
            
                return true;
            }

            return false;
        }
        public void FillListOfWinningBets(List<Bet> listBets)
        {
            var winningResult = GetWining();
            foreach (var bet in listBets)
            {
                var winningBet = new WinningBet();
                if (bet.Type_Bet == "color")
                {
                    if (bet.Value_Bet == winningResult.Color)
                    {
                        winningBet.Bet = bet;
                        winningBet.EarnedMoney = bet.Bet_Amount * 1.8;
                        _listWinningBet.Add(winningBet);
                    }
                }
                else
                {
                    if (Convert.ToInt16(bet.Value_Bet) == winningResult.Number)
                    {
                        winningBet.Bet = bet;
                        winningBet.EarnedMoney = bet.Bet_Amount * 5;
                        _listWinningBet.Add(winningBet);
                    }
                }
            }
            _successCloseBetsResult.Bets = listBets;
            _successCloseBetsResult.WinningBets = _listWinningBet;
        }
        public WiningResult GetWining()
        {
            var winningBetModel = new WiningResult();
            int numberWinning = _globalFunctions.GetRandomNumber();
            string colorWinning = _globalFunctions.GetColorAccordingToNumber(numberWinning);
            winningBetModel.Number = numberWinning;
            winningBetModel.Color = colorWinning;

            return winningBetModel;
        }
    }
}
