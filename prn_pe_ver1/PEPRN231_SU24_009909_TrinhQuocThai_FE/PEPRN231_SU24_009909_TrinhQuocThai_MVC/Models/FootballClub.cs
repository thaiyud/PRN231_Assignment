﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PEPRN231_SU24_009909_TrinhQuocThai_MVC.Models;

public partial class FootballClub
{
    public string FootballClubId { get; set; }

    public string ClubName { get; set; }

    public string ClubShortDescription { get; set; }

    public string SoccerPracticeField { get; set; }

    public string Mascos { get; set; }

    public virtual ICollection<FootballPlayer> FootballPlayers { get; set; } = new List<FootballPlayer>();
}