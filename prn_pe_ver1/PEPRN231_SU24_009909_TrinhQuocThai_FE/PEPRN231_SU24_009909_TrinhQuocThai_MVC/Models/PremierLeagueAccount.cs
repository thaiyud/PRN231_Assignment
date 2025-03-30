
#nullable disable
using System;
using System.Collections.Generic;

namespace PEPRN231_SU24_009909_TrinhQuocThai_MVC.Models;

public partial class PremierLeagueAccount
{
    public int AccId { get; set; }

    public string Password { get; set; }

    public string EmailAddress { get; set; }

    public string Description { get; set; }

    public int? Role { get; set; }
}