namespace PracticeLogApi
{
    public class PracticeSession
    {
        public int Id { get; set; }
        public string? SongTitle { get; set; }
        public string? Tuning { get; set; }
        public int TargetBpm { get; set; }
        public bool IsMastered { get; set; }
        public string? SecretNotes { get; set; }
    }
}