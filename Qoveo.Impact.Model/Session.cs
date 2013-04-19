using System;
using System.Collections;
using System.Collections.Generic;

namespace Qoveo.Impact.Model
{
    /// <summary>
    /// Entity wich represent a session
    /// </summary>
    public class Session : ImpactEntity
    {
        /// <summary>
        /// The Session's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Session's Start Date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// The Session's End Date
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// The Session's Mission 1 Start date
        /// </summary>
        public DateTime? Mission1StartDate { get; set; }
        /// <summary>
        /// The Session's Mission 1 End date
        /// </summary>
        public DateTime? Mission1EndDate { get; set; }
        /// <summary>
        /// The Session's Mission 2 Start date
        /// </summary>
        public DateTime? Mission2StartDate { get; set; }
        /// <summary>
        /// The Session's Mission 2 End date
        /// </summary>
        public DateTime? Mission2EndDate { get; set; }
        /// <summary>
        /// The Session's Mission 3 Start date
        /// </summary>
        public DateTime? Mission3StartDate { get; set; }
        /// <summary>
        /// The Session's Mission 3 End date
        /// </summary>
        public DateTime? Mission3EndDate { get; set; }
        /// <summary>
        /// The Session's Mission 4 Start date
        /// </summary>
        public DateTime? Mission4StartDate { get; set; }
        /// <summary>
        /// The Session's Mission 4 En date
        /// </summary>
        public DateTime? Mission4EndDate { get; set; }

        #region Navigation Properties
        public ICollection<Team> TeamList { get; set; }
        #endregion
    }
}
