using TaskManager.Models;
using TaskManager.Controllers;
using TaskManager.Services;

namespace TaskManager.Data
{
    public class DbInitializer
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public DbInitializer(IAuthService authService, AppDbContext db, IConfiguration config)
        {
            _authService = authService;
            _db = db;
            _config = config;
        }

        public async System.Threading.Tasks.Task InitializeAsync()
        {
            if (!_db.Users.Any(u => u.Role.ToString() == "Admin"))
            {
                if (string.IsNullOrEmpty(_config["FirstAdmin:Login"]) || string.IsNullOrEmpty(_config["FirstAdmin:Password"]))
                {
                    Console.WriteLine("FirstAdmin:Login or Password is missing in appsettings.json");
                    return;
                }

                _authService.CreatePasswordHash(_config["FirstAdmin:Password"]!, out byte[] hash, out byte[] salt);

                var user = new User
                {
                    Id = 1,
                    UserName = _config["FirstAdmin:Login"],
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Role = UserRole.Admin,
                };

                _db.Users.Add(user);

                Console.WriteLine("[!] First Admin user created");

                await _db.SaveChangesAsync();
            }
        }
    }
}
