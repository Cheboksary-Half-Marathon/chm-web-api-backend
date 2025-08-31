using CheboksaryHalfMarathon.DAL;
using CheboksaryHalfMarathon.WebAplication.API;
using CheboksaryHalfMarathon.WebAplication.Config;
using CheboksaryHalfMarathon.WebAplication.DTO;
using CheboksaryHalfMarathon.WebAplication.Helpers;
using CheboksaryHalfMarathon.WebAplication.OData;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CheboksaryHalfMarathon.WebAplication.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConventionModelFactory _modelFactory;
        private readonly IOptionsMonitor<JwtConfig> _config;

        private const int repeatingHash = 7;

        public UserController(
            IUnitOfWork uow,
            IConventionModelFactory modelFactory,
            IOptionsMonitor<JwtConfig> config)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [HttpPost]
        [Route("register")]
        public async Task<string> Register([FromBody] UserCreationOptionsDto userCreationOptions)
        {
            if (userCreationOptions == null)
                throw new ArgumentNullException(nameof(userCreationOptions));
            if (string.IsNullOrWhiteSpace(userCreationOptions.UserEmail))
                throw new ArgumentNullException(nameof(userCreationOptions.UserEmail));
            if(string.IsNullOrWhiteSpace(userCreationOptions.UserPassword))
                throw new ArgumentNullException(nameof(userCreationOptions.UserPassword));
            if (string.IsNullOrWhiteSpace(userCreationOptions.UserRole))
                throw new ArgumentNullException(nameof(userCreationOptions.UserRole));

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userCreationOptions.UserPassword, repeatingHash);

            var user = Domain.Models.User.Save(
                userCreationOptions.UserEmail,
                hashPassword,
                userCreationOptions.UserRole);

            await _uow.BeginTransactionAsync();
            _uow.UserRepository.AddToContext(user);
            await _uow.CommitAsync();

            var token = this.GenerateToken();
            return token;
        }

        private string GenerateToken()
        {
            var secretKey = _config.CurrentValue.SecretKey;
            var token = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(secretKey)
                .Encode();

            return token;
        }
    }
}
