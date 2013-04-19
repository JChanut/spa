using System;
using System.Collections.Generic;

namespace Qoveo.Impact.Model
{
    /// <summary>
    /// Entity that represent a tutor
    /// </summary>
    public class Tutor : ImpactEntity
    {
        #region Properties
        public string Name { get; set; }
        public string FirstName { get; set; }
        public int SessionId { get; set; }
        public int ClusterId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        #endregion

        #region Navigation Properties
        public Session Session { get; set; }
        public Cluster Cluster { get; set; }
        #endregion
    }
}
