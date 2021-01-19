using bettingRouletteAPI.Entities;
namespace bettingRouletteAPI.Helpers.Results
{
    public class WinningBet
    {
        public Bet Bet { get; set; }
        public double EarnedMoney { get; set; }
    }
}
