using Microsoft.EntityFrameworkCore;
using PEPRN231_SU24_009909_Repo;
using PEPRN231_SU24_009909_Repo.Models;
using PEPRN231_SU24_009909_Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PEPRN231_SU24_009909_Service
{
    public class FootballClubService
    {

        private readonly IBaseRepository<FootballClub> _repo;

        public FootballClubService(IBaseRepository<FootballClub> repo)
        {
            _repo = repo;
      
        }
        public async Task<IEnumerable<FootballClub>> Get()
        {
            return await _repo.Get();
        }

        public async Task<FootballClub> GetById(string id)
        {
            return await _repo.GetById(id , "FootballClubId");
        }

    }
}
