namespace PracticeLogApi
{
    public class PracticeSessionDTO
    {
        public int Id { get; set; }
        public string? SongTitle { get; set; }
        public string? Tuning { get; set; }
        public int TargetBpm { get; set; }
        public bool IsMastered { get; set; }

        public PracticeSessionDTO() { }

        public PracticeSessionDTO(PracticeSession session) =>
            (Id, SongTitle, Tuning, TargetBpm, IsMastered) =
            (session.Id, session.SongTitle, session.Tuning, session.TargetBpm, session.IsMastered);
    }
}