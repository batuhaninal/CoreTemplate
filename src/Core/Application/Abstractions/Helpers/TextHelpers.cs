namespace Application.Abstractions.Helpers
{
    public static class TextHelpers
    {
        public static string CreateUniqueText() 
        {
            DateTime currentTime = DateTime.Now;
            return $"{currentTime.Year}{currentTime.Month}{currentTime.Day}{currentTime.Hour}{currentTime.Minute}{currentTime.Second}{currentTime.Millisecond}";
        }
    }
}