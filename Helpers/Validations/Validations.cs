using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Results;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
namespace bettingRouletteAPI.Helpers.Validations
{
    public class Validations
    {
        public string ValidateIfTokenExist(HttpRequest request, TokensModel tokensModel)
        {
            string response = "";
            string tokenString = "";
            foreach (var item in request.Headers)
            {
                if (item.Key.Equals("Authorization"))
                {
                    tokenString = item.Value.First();
                    break;
                }
            }
            if (tokenString != "") {
                var token = tokensModel.GetTokenByStringToken(tokenString);
                response = (token != null) ? "Ok" : ""; 
            }

            return response;
        }
        public ResultValidationBet ValidateBet(Bet bet, RoulettesModel rouletteModel)
        {
            var resultValidationBet = new ResultValidationBet();
            resultValidationBet.Status = true;
            if (bet.Value_Bet == "" || bet.Type_Bet == "")
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "Value_Bet and Type_Bet is required.";
            } else
            {
                var result = ValidateTypeBet(bet);
                if (result.Status == false)
                {
                    resultValidationBet = result;
                }
            }
            if (bet.Bet_Amount.ToString() == null || bet.Bet_Amount.ToString() == "")
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "Bet_Amount is required.";
            } else
            {
                var result = ValidateBetAmount(bet.Bet_Amount);
                if (result.Status == false)
                {
                    resultValidationBet = result;
                }
            }
            if (bet.Id_Roulette == null || bet.Id_Roulette == "")
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "Id_Roulette is required.";
            }

            return resultValidationBet;
        }
        public ResultValidationBet ValidateBetAmount(int amount)
        {
            var resultValidationBet = new ResultValidationBet();
            resultValidationBet.Status = true;
            if (amount < 0)
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "Invalid amount";
            }
            if (amount > 10000)
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "The maximum amount is 10000 USD";
            }

            return resultValidationBet;
        }
        public ResultValidationBet ValidateIfExistRouletteOpen(Bet bet, RoulettesModel rouletteModel)
        {
            var resultValidationBet = new ResultValidationBet();
            if (bet.Id_Roulette == null || bet.Id_Roulette == "")
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "Id_Roulette is required.";
            }
            else
            {
                if (bet.Id_Roulette.Length == 24)
                {
                    var roulette = rouletteModel.GetRouletteById(bet.Id_Roulette);
                    if (roulette != null)
                    {
                        resultValidationBet.Status = (roulette.Status == "Open") ? true : false;
                        resultValidationBet.Message = (roulette.Status == "Open") ? "" : "Roulette is not open";
                    }
                    else
                    {
                        resultValidationBet.Status = false;
                        resultValidationBet.Message = "Roulette not found";
                    }
                } else
                {
                    resultValidationBet.Status = false;
                    resultValidationBet.Message = "The Id_Roulette is not valid";
                }
            }

            return resultValidationBet;
        }
        public ResultValidationBet ValidateTypeBet(Bet bet)
        {
            var resultValidationBet = new ResultValidationBet();
            resultValidationBet.Status = true;
            if (bet.Type_Bet == "number")
            {
                try
                {
                    int valueBet = Convert.ToInt16(bet.Value_Bet);
                    if (valueBet >= 0 && valueBet <= 36)
                    {
                        resultValidationBet.Status = true;
                    } else
                    {
                        resultValidationBet.Status = false;
                        resultValidationBet.Message = "Value must be between the numbers 0 and 36";
                    }
                }
                catch (Exception ex)
                {
                    resultValidationBet.Status = false;
                    resultValidationBet.Message = "The value is not a valid number";
                    Console.WriteLine(ex.Message);
                }

            } else if (bet.Type_Bet == "color")
            {
                if (bet.Value_Bet == "rojo" || bet.Value_Bet == "negro")
                {
                    resultValidationBet.Status = true;
                }
                else
                {
                    resultValidationBet.Status = false;
                    resultValidationBet.Message = "The color is not valid. Valid colors ​​('negro' or 'rojo')";
                }
            } else
            {
                resultValidationBet.Status = false;
                resultValidationBet.Message = "The type of bet is not valid. Valid values ​​('number' or 'color')";
            }

            return resultValidationBet;
        }
    }
}
