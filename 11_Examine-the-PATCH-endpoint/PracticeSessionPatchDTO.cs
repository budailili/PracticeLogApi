namespace PracticeLogApi
{
    public class PracticeSessionPatchDTO
    {
        public string? SongTitle { get; set; }
        public string? Tuning { get; set; }
        public int? TargetBpm { get; set; }
        public bool? IsMastered { get; set; }
    }
}