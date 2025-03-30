using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PEPRN231_SU24_009909_TrinhQuocThai_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Azure;

namespace PEPRN231_SU24_009909_TrinhQuocThai_MVC.Controllers
{
    public class FootballPlayersController : Controller
    {
        private readonly List<string> SomePermisson = new List<string> { "2" };
        private readonly string AllPermisson = "1";
        private readonly string NonePermission = "2";
        private string APIEndPoint = "https://localhost:7140/api/";
  

        public FootballPlayersController()
        {
         
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role) && !SomePermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "FootballPlayer/get"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<FootballPlayerVM>>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return View(new List<FootballPlayerVM>());
        }
        public async Task<IActionResult> Details(string id)
        {
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role) && !SomePermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "FootballPlayer/getById?id=" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<FootballPlayerVM>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new FootballPlayerVM());
        }

        public async Task<List<FootballClub>> GetFootballClub()
        {
            var item = new List<FootballClub>();
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "FootballClub/get"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<List<FootballClub>>(content);
                        return item;
                    }
                }
            }

            return new List<FootballClub>();
        }
        public async Task<IActionResult> Create()
        {
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }
            ViewData["FootballClubId"] = new SelectList(await this.GetFootballClub(), "FootballClubId", "ClubName");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FootballPlayer item)
        {
            var saveStatus = false;
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        #region Add Token to header of Request

                        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                        #endregion


                        using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "FootballPlayer/add", item))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<int>(content);

                                if (result > 0)
                                {
                                    saveStatus = true;
                                }
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["FootballClubId"] = new SelectList(await this.GetFootballClub(), "FootballClubId", "ClubName");
                return View(new FootballPlayer());
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "FootballPlayer/getById?id=" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<FootballPlayer>(content);

                        if (result != null)
                        {

                            ViewData["FootballClubId"] = new SelectList(await this.GetFootballClub(), "FootballClubId", "ClubName" , result.FootballClubId);
                            return View(result);
                        }
                    }
                }
            }
            ViewData["FootballClubId"] = new SelectList(await this.GetFootballClub(), "FootballClubId", "ClubName");
            return View(new FootballPlayer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FootballPlayer item)
        {
            var saveStatus = false;
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        #region Add Token to header of Request

                        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                        #endregion


                        using (var response = await httpClient.PutAsJsonAsync(
    $"{APIEndPoint}FootballPlayer/update?id={item.FootballPlayerId}",
    item
)
)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<int>(content);

                                if (result > 0)
                                {
                                    saveStatus = true;
                                }
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["FootballClubId"] = new SelectList(await this.GetFootballClub(), "FootballClubId", "ClubName", item.FootballClubId);
                return View(item);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "FootballPlayer/getById?id=" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<FootballPlayer>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new FootballPlayer());
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuickly(string id)
        {
            bool deleteStatus = false;
            var role = HttpContext.Request.Cookies["Role"];
            if (role == null || (role != null && !AllPermisson.Contains(role)))
            {
                return RedirectToAction("Forbidden", "PremierLeagueAccounts");
            }
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.DeleteAsync(APIEndPoint + "FootballPlayer/delete" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        deleteStatus = true;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
