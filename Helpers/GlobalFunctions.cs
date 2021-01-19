using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics;


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
        public void Logger(object obj, string message)
        {
            StackTrace stacktrace = new StackTrace();
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            var lineOne = "Class:" + obj.GetType().FullName + " " + DateTime.Now;
            var lineTwo = "Method: " + stacktrace.GetFrame(1).GetMethod().Name + " - " + message;
            logger.Info(lineOne);
            logger.Info(lineTwo);
        }
    }
}
