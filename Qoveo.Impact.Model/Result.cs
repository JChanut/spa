using System;

namespace Qoveo.Impact.Model
{
    /// <summary>
    /// A class representing a case of a <see cref="Team"/> attending a <see cref="Mission"/>.
    /// A many-to-many link between <see cref="Mission"/> and <see cref="Team"/>
    /// </summary>
    public class Result
    {
        #region properties
        public int TeamId { get; set; }
        public int MissionId { get; set; }
        public float Jauge1 { get; set; }
        public float Jauge2 { get; set; }
        public float Jauge3 { get; set; }
        public float Jauge4 { get; set; }
        public float Jauge5 { get; set; }
        public int ScoreEpreuve1 { get; set; }
        public int ScoreEpreuve2 { get; set; }
        public int ScoreEpreuve3 { get; set; }
        public int GlobalScore { get; set; }
        #endregion

        #region Navigation properties
        public Team Team { get; set; }
        public Mission Mission { get; set; }
        #endregion
    }
}
