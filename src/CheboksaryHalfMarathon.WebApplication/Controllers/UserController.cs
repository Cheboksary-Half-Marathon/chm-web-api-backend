using CheboksaryHalfMarathon.DAL;
using CheboksaryHalfMarathon.WebAplication.API;
using CheboksaryHalfMarathon.WebAplication.DTO;
using CheboksaryHalfMarathon.WebAplication.Helpers;
using CheboksaryHalfMarathon.WebAplication.OData;
using Microsoft.AspNetCore.Mvc;

namespace CheboksaryHalfMarathon.WebAplication.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConventionModelFactory _modelFactory;

        public UserController(
            IUnitOfWork uow,
            IConventionModelFactory modelFactory)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        }

        [HttpPost]
        [Route("register")]
        public async Task<ApiResultT<UserDto>> Post([FromBody] UserCreationOptionsDto userCreationOptions)
        {
            if (userCreationOptions == null)
                throw new ArgumentNullException(nameof(userCreationOptions));
            if (string.IsNullOrWhiteSpace(userCreationOptions.UserEmail))
                throw new ArgumentNullException(nameof(userCreationOptions.UserEmail));
            if(string.IsNullOrWhiteSpace(userCreationOptions.UserPassword))
                throw new ArgumentNullException(nameof(userCreationOptions.UserPassword));
            if (string.IsNullOrWhiteSpace(userCreationOptions.UserRole))
                throw new ArgumentNullException(nameof(userCreationOptions.UserRole));

            var user = Domain.Models.User.Save(
                userCreationOptions.UserEmail,
                userCreationOptions.UserPassword,
                userCreationOptions.UserRole);

            await _uow.BeginTransactionAsync();
            _uow.UserRepository.AddToContext(user);
            await _uow.CommitAsync();

            return user
                .ToDto()
                .ToApiResult();
        }
    }
}
