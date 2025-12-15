using FU_House_Finder.Repositories;
using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Services
{

    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;

        public RateService(IRateRepository rateRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _rateRepository = rateRepository;
            _httpFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<object>> GetRatesByHouseIdAsync(int houseId)
        {
            var rates = (await _rateRepository.GetRatesByHouseIdAsync(houseId)).ToList();

            var authBase = _configuration["AuthService:BaseUrl"]?.TrimEnd('/') ?? throw new InvalidOperationException("AuthService:BaseUrl not configured");
            var client = _httpFactory.CreateClient();

            var result = new List<object>();

            foreach (var r in rates)
            {
                string studentName = "Unknown";
                try
                {
                    var url = $"{authBase}/api/users/{r.StudentId}";
                    var resp = await client.GetAsync(url);
                    if (resp.IsSuccessStatusCode)
                    {
                        var user = await resp.Content.ReadFromJsonAsync<UserNameDto>();
                        if (user != null) studentName = user.FullName;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception fetching user {r.StudentId}: {ex.Message}");
                }

                result.Add(new
                {
                    r.Id,
                    r.HouseId,
                    r.StudentId,
                    StudentName = studentName,
                    r.Star,
                    r.Comment,
                    r.CreatedDate
                });
            }

            return result;
        }

        public async Task<Rate> CreateRateAsync(Rate rate)
        {
            return await _rateRepository.CreateRateAsync(rate);
        }
        private class UserNameDto
        {
            public int Id { get; set; }
            public string FullName { get; set; } = string.Empty;
        }

        
    }
}

