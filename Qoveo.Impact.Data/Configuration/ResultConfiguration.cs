using Qoveo.Impact.Model;
using System.Data.Entity.ModelConfiguration;

namespace Qoveo.Impact.Data.Configuration
{
    public class ResultConfiguration : EntityTypeConfiguration<Result>
    {
        public ResultConfiguration()
        {
            // Result has a composite key : SessionId and MissionId
            HasKey(a => new { a.MissionId, a.TeamId });

            
        }
    }
}
