using System.Collections;
using System.Collections.Generic;

namespace Qoveo.Impact.Model
{
    public class Team : ImpactEntity
    {
        #region properties
        /// <summary>
        /// Team's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Team's Session Id
        /// </summary>
        public int SessionId { get; set; }
        public int? ClusterId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// The Team's Session
        /// </summary>
        public Session Session { get; set; }
        public Cluster Cluster { get; set; }
        public ICollection<Result> ResultList { get; set; }
        #endregion
    }
}
