using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEPRN231_SU24_009909_Repo
{
   public  class FootballPlayerVM
    {
        public string FootballPlayerId { get; set; } 

        public string FullName { get; set; }

        public string ClubName { get; set; }
        public string Achievements { get; set; }

        public DateTime? Birthday { get; set; }

        public string PlayerExperiences { get; set; }

        public string Nomination { get; set; }

        public string FootballClubId { get; set; }
    }
}
