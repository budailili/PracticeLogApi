using Microsoft.EntityFrameworkCore;

namespace PracticeLogApi
{
    public class PracticeLogDb : DbContext
    {
        public PracticeLogDb(DbContextOptions<PracticeLogDb> options)
            : base(options) { }

        public DbSet<PracticeSession> Sessions => Set<PracticeSession>();
    }
}