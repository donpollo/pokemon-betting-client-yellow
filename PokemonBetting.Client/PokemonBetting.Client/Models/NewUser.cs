using System;
using System.Text.RegularExpressions;
using FluentValidation;

namespace PokemonBetting.Client.Models
{
    public class NewUser : ModelBase<NewUser>
    {
        public string UserName { get; }
        public string EMail { get; }
        public string Password { get; }

        //Throws ValidationException if params are not valid
        public NewUser(string userName, string email, string pw, string pwCheck)
        {
            UserName = userName;
            EMail = email;
            Password = pw;
            
            var v = new UserValidator(pwCheck);
            v.ValidateAndThrow(this);
        }
    }

    //checks if all the instance variables in a User instance are valid
    internal class UserValidator : AbstractValidator<NewUser>
    {
        private const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public UserValidator(string pwCheck)
        {
            //all the validation rules
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Please specify a user name");
            RuleFor(user => user.EMail).Must(BeAValidEmail).WithMessage("Please specify an email adress");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Please specify a password").Equal(pwCheck).WithMessage("You entered two different passwords");
        }

        private static bool BeAValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}