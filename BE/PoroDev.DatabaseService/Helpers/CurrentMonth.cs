namespace PoroDev.DatabaseService.Helpers
{
    public static class CurrentMonth
    {
        public static string GetMonthName()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("MMMM");
        }
    }
}
