using Qoveo.Impact.Model;
using System.Data.Entity.ModelConfiguration;

namespace Qoveo.Impact.Data.Configuration
{
    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            Property(p => p.StartDate).IsRequired();
            Property(p => p.Name).IsRequired();
            Property(p => p.Mission1StartDate).IsOptional();
            Property(p => p.Mission1EndDate).IsOptional();
            Property(p => p.Mission2StartDate).IsOptional();
            Property(p => p.Mission2EndDate).IsOptional();
            Property(p => p.Mission3StartDate).IsOptional();
            Property(p => p.Mission3EndDate).IsOptional();
            Property(p => p.Mission4StartDate).IsOptional();
            Property(p => p.Mission4EndDate).IsOptional();
        }
    }
}
