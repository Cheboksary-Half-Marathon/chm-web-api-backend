using CheboksaryHalfMarathon.Domain.Models;
using CheboksaryHalfMarathon.WebAplication.DTO;

namespace CheboksaryHalfMarathon.WebAplication.Helpers
{
    public static class UserExtentions
    {
        public static UserDto ToDto(this User requestCardModel)
        {
            return new UserDto()
            {
                UserId = requestCardModel.UserId,
                UserEmail = requestCardModel.UserEmail,
                UserPassword = requestCardModel.UserPassword,
                UserRegistrationDate = requestCardModel.UserRegistrationDate,
                UserRole = requestCardModel.UserRole,
                UserVersion = requestCardModel.UserVersion
            };
        }
    }
}
