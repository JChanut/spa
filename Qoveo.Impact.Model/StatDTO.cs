using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qoveo.Impact.Model
{
    public class StatDTO
    {
        public int SessionId { get; set; }
        public int TeamId { get; set; }
        public int ClusterId { get; set; }
        public int MissionId { get; set; }

        public string Session { get; set; }
        public string Team { get; set; }
        public string Cluster { get; set; }
        public string Mission { get; set; }

        public float Jauge1 { get; set; }
        public float Jauge2 { get; set; }
        public float Jauge3 { get; set; }
        public float Jauge4 { get; set; }
        public float Jauge5 { get; set; }

        public int ScoreEpreuve1 { get; set; }
        public int ScoreEpreuve2 { get; set; }
        public int ScoreEpreuve3 { get; set; }

        public int GlobalScore { get; set; }
    }
}
