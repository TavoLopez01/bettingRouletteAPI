using System;
namespace bettingRouletteAPI.Helpers
{
    public class GlobalFunctions
    {
        public DateTime GetDateFromFormat(string format)
        {
            var date = DateTimeOffset.Now.ToString(format);

            return DateTime.Parse(date);
        }
        public int GetRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 36);

            return randomNumber;
        }
        public string GetColorAccordingToNumber(int number)
        {
            return (number % 2 == 0) ? "rojo" : "negro";
        }
    }
}
