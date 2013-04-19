using Qoveo.Impact.Model;
using System.Data.Entity.ModelConfiguration;

namespace Qoveo.Impact.Data.Configuration
{
    public class SurveyConfiguration : EntityTypeConfiguration<Survey>
    {
        public SurveyConfiguration()
        {
            // Survey has a composite key : SessionId, MissionId and Question
            HasKey(a => new { a.MissionId, a.TeamId, a.Question });
        }
    }
}
