using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoveo.Impact.Model
{
    /// <summary>
    /// A class representing a case of a <see cref="Team"/> answering the questions survey in a <see cref="Mission"/>.
    /// A many-to-many link between <see cref="Mission"/> and <see cref="Team"/>
    /// </summary>
    public class Survey
    {
        #region Properties
        public int TeamId { get; set; }
        public int MissionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        #endregion

        #region Navigation Properties
        public Team Team { get; set; }
        public Mission Mission { get; set; }
        #endregion

    }
}
