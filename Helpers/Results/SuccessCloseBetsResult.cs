using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Results;
using System.Collections.Generic;
namespace bettingRouletteAPI.Model
{
    public class SuccessCloseBetsResult
    {
        public List<Bet> Bets { get; set; }
        public List<WinningBet> WinningBets { get; set; }
    }
}
