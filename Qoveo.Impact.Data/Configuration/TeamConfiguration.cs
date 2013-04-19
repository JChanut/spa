using Qoveo.Impact.Model;
using System.Data.Entity.ModelConfiguration;

namespace Qoveo.Impact.Data.Configuration
{
    public class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            Property(p => p.ClusterId).IsOptional();
        }
    }
}
