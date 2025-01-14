using Microsoft.EntityFrameworkCore;
using Website.Components.Models;

namespace Website.Components.Data
{
    public class CsProjectContext : DbContext
    {
        public CsProjectContext(DbContextOptions<CsProjectContext> options) : base(options)
        {
        }
        public DbSet<CsProject> CsProjects => Set<CsProject>();
    }
}
