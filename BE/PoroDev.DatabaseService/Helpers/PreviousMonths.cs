namespace PoroDev.DatabaseService.Helpers
{
    public class PreviousMonths
    {
        public static List<string> GetPreviousMonths(DateTime dt, int numberOfRequiredPreviousMonths)
        {
            List<string> previousMonths = new List<string>();
            int tempCounter = 1;
            while(tempCounter <= numberOfRequiredPreviousMonths)
            {
                previousMonths.Add(GetPreviousMonthName(dt.AddMonths(-tempCounter)));
                tempCounter++;
            }

            return previousMonths;
        }

        public static string GetPreviousMonthName(DateTime dt)
        {
            return dt.ToString("MMMM");
        }
    }
}
