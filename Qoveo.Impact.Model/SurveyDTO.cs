using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoveo.Impact.Model
{
    public class SurveyDTO
    {
        public int SessionId { get; set; }
        public int TeamId { get; set; }
        public int ClusterId { get; set; }
        public int MissionId { get; set; }

        public string Session { get; set; }
        public string Team { get; set; }
        public string Cluster { get; set; }
        public string Mission { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
