namespace Planer_Lekcyjny_TEB.Server.Classes
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
