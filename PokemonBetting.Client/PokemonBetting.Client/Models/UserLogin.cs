using System.Net.Http;
using System.Text;
using FluentValidation;
using PokemonBetting.Client.Helpers;

namespace PokemonBetting.Client.Models
{
    public class UserLogin
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

        //Use this to convert a user to json and send it to the server
        public StringContent ToJson()
        {
            var json = LowerCaseSerializer.SerializeObject(this);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

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