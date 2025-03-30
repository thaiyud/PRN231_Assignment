
using System;
using System.Collections.Generic;

namespace PEPRN231_SU24_009909_TrinhQuocThai_MVC.Models;

public partial class FootballPlayerVM
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