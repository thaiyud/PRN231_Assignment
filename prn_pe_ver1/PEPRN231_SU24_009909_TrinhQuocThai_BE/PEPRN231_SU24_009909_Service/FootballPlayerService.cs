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
    public class FootballPlayerService
    {
        private readonly IBaseRepository<FootballPlayer> _repo;
        private readonly IBaseRepository<FootballClub> _clubRepo;

        public FootballPlayerService(IBaseRepository<FootballPlayer> repo, IBaseRepository<FootballClub> categoryRepo)
        {
            _repo = repo;
            _clubRepo = categoryRepo;
        }
        public async Task<IEnumerable<FootballPlayerVM>> Get()
        {
            var items = await _repo.Get(a => a.Include(b => b.FootballClub));
            var result = items.ToList().Select(a => new FootballPlayerVM()
            {
                ClubName = a.FootballClub.ClubName,
                FootballPlayerId = a.FootballPlayerId,
                FullName = a.FullName,
                Achievements = a.Achievements,
                Birthday = a.Birthday,
                PlayerExperiences = a.PlayerExperiences,
                Nomination = a.Nomination,
                FootballClubId = a.FootballClubId,
            });
            return result;
        }

        public async Task<FootballPlayerVM> GetById(string id)
        {
            var exist = await _repo.GetById(id, "FootballPlayerId", a => a.Include(b => b.FootballClub));
            if (exist == null)
            {
                return new FootballPlayerVM();
            }
            return new FootballPlayerVM
            {
                Nomination = exist.Nomination,
                Achievements = exist.Achievements,
                FullName = exist.FullName,
                Birthday = exist.Birthday,
                PlayerExperiences = exist.PlayerExperiences,
                FootballClubId = exist.FootballClubId,
                ClubName = exist.FootballClub.ClubName,
                FootballPlayerId = exist.FootballPlayerId
            };
        }
        public async Task<string> Add(FootballPlayer request)
        {
            if (string.IsNullOrWhiteSpace(request.FootballPlayerId) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Achievements) ||
                string.IsNullOrWhiteSpace(request.Birthday.ToString()) ||
                string.IsNullOrWhiteSpace(request.Nomination)
                )
            {
                return "All fields are required.";

            }

            if (!Regex.IsMatch(request.FullName, @"^([A-Z][a-zA-Z0-9@#]*\s)*[A-Z][a-zA-Z0-9@#]*$"))
            {
                return "FullName must start with a capital letter in each word and only include letters, numbers, space, @, and #.";
            }

            DateTime minBirthday = new DateTime(2007, 1, 1);
            if (request.Birthday >= minBirthday)
            {
                return "Birthday must be before 01-01-2007.";
            }
            // Validate length
            if (request.Achievements.Length < 9 || request.Achievements.Length > 100 ||
                request.Nomination.Length < 9 || request.Nomination.Length > 100)
            {
                return "Achievements and Nomination must be between 9 and 100 characters.";
            }

            if (await _clubRepo.GetById(request.FootballClubId, "FootballClubId") == null)
            {
                return "khong ton tai!";

            }

            FootballPlayer item = new FootballPlayer()
            {
                FootballPlayerId = request.FootballPlayerId,
                FootballClubId = request.FootballClubId,
                FullName = request.FullName.Trim(),
                Achievements = request.Achievements,
                PlayerExperiences = request.PlayerExperiences.Trim(),
                Birthday = request.Birthday,
                Nomination = request.Nomination,
            };

            await _repo.Add(item);
            return "Them thanh cong!";
        }
       
        public async Task<string> Update(string id, FootballPlayer request)
        {
            var existedInfo = await _repo.GetById(id, "FootballPlayerId");
            if (existedInfo == null)
            {
                return "Khong ton tai";
            }

            if (string.IsNullOrWhiteSpace(request.FootballPlayerId) ||
              string.IsNullOrWhiteSpace(request.FullName) ||
              string.IsNullOrWhiteSpace(request.Achievements) ||
              string.IsNullOrWhiteSpace(request.Birthday.ToString()) ||
              string.IsNullOrWhiteSpace(request.Nomination)
              )
            {
                return "All fields are required.";

            }

            if (!Regex.IsMatch(request.FullName, @"^([A-Z][a-zA-Z0-9@#]*\s)*[A-Z][a-zA-Z0-9@#]*$"))
            {
                return "FullName must start with a capital letter in each word and only include letters, numbers, space, @, and #.";
            }

            // Validate length
            if (request.Achievements.Length < 9 || request.Achievements.Length > 100 ||
                request.Nomination.Length < 9 || request.Nomination.Length > 100)
            {
                return "Achievements and Nomination must be between 9 and 100 characters.";
            }

            if (await _clubRepo.GetById(request.FootballClubId, "FootballClubId") == null)
            {
                return "khong ton tai!";

            }



            existedInfo.FullName = request.FullName;
            existedInfo.FootballClubId = request.FootballClubId;
            existedInfo.Achievements = request.Achievements;
            existedInfo.Birthday = request.Birthday;
            existedInfo.PlayerExperiences = request.PlayerExperiences;
            existedInfo.Nomination = request.Nomination;

            await _repo.Update(existedInfo);
            return "Cap nhat thanh cong!";
        }

        public async Task<string> Delete(string id)
        {
            var existedInfo = await _repo.GetById(id, "FootballPlayerId");
            if (existedInfo == null)
            {
                return "Khong ton tai";
            }
            await _repo.Delete(existedInfo);
            return "Xoa thanh cong!";
        }

        public async Task<IEnumerable<FootballPlayerVM>> Search()
        {
            var groupedResults = await _repo.Entities.Include(x => x.FootballClub)
                .Select(g => new FootballPlayerVM
                {
                    FootballPlayerId = g.FootballPlayerId,
                    FullName = g.FullName,
                    ClubName = g.FootballClub.ClubName,
                    Achievements = g.Achievements,
                    Birthday = g.Birthday,
                    PlayerExperiences = g.PlayerExperiences,
                    Nomination = g.Nomination,
                    FootballClubId = g.FootballClubId,

                })
                .ToListAsync(); 

            return groupedResults;
        }
    }
}
