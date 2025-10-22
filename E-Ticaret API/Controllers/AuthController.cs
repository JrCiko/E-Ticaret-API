using E_Ticaret_API.Data;
using E_Ticaret_API.DTOs;
using E_Ticaret_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Ticaret_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // appsettings.json'ı okumak için
        }

        // --- KAYIT OL ENDPOINT'İ ---
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterDto request)
        {
            // BCrypt kullanarak şifreyi hash'le (artık direkt string)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash // Direkt string olarak ata
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // --- GİRİŞ YAP ENDPOINT'İ ---
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            // 1. Kullanıcıyı bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // 2. Şifreyi Doğrula (BCrypt.Verify kullanarak)
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            // 3. Şifre doğruysa JWT Token oluştur
            string token = CreateToken(user);

            return Ok(token);
        }

        // --- TOKEN OLUŞTURMA METODU ---
        private string CreateToken(User user)
        {
            // Token'ın içinde saklanacak bilgiler (Claims)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
                // İstersen buraya "Role" (Rol) gibi başka bilgiler de ekleyebilirsin
            };

            // appsettings.json'dan gizli anahtarı al
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            // Anahtarı kullanarak token'ı imzala
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Token'ın geçerlilik süresi vb. ayarları
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), // Token 1 gün geçerli olsun
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Token'ı string formatında dön
            return tokenHandler.WriteToken(token);
        }
    }
}
