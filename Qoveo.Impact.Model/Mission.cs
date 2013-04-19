using System.Collections;
using System.Collections.Generic;

namespace Qoveo.Impact.Model
{
    public class Mission : ImpactEntity
    {
        #region properties
        public string Name { get; set; }
        public int Order { get; set; }
        public string MissionUrl { get; set; }
        public string FtaPdfUrl { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<Result> ResultList { get; set; }
        #endregion
    }
}
