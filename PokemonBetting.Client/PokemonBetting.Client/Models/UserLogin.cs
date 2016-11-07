using FluentValidation;

namespace PokemonBetting.Client.Models
{
    public class UserLogin : ModelBase<UserLogin>
    {
        public UserLogin(string username, string password)
        {
            Username = username;
            Password = password;

            var v = new UserLoginValidator();
            v.ValidateAndThrow(this);
        }

        public string Username { get; }

        public string Password { get; }

        private class UserLoginValidator : AbstractValidator<UserLogin>
        {
            public UserLoginValidator()
            {
                RuleFor(userLogin => userLogin.Username).NotEmpty().WithMessage("Please specify a user name");
                RuleFor(user => user.Password).NotEmpty().WithMessage("Please specify a password");
            }
        }
    }
}