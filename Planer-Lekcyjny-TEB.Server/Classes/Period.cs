namespace Planer_Lekcyjny_TEB.Server.Classes
{
    public class Period
    {
        public string Name { get; set; }
        public int PeriodNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
