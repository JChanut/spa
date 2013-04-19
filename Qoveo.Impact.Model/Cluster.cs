using System;
using System.Collections;
using System.Collections.Generic;

namespace Qoveo.Impact.Model
{
    public class Cluster : ImpactEntity
    {
        #region properties
        /// <summary>
        /// The cluster's name
        /// </summary>
        public string Name { get; set; }
        public string PdfUrl { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<Tutor> TutorList { get; set; }
        #endregion
    }
}
