using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PokemonBetting.Client.Helpers;

namespace PokemonBetting.Client.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

        //Throws ValidationException if params are not valid
        public User(String UserName, String Email, String pw, String pwCheck)
        {
            this.UserName = UserName;
            this.EMail = Email;
            this.Password = pw;
            
            UserValidator v = new UserValidator(pwCheck);
            v.ValidateAndThrow(this);
        }

        //Use this to convert a user to json and send it to the server
        public StringContent ToJson()
        {
            var json = LowerCaseSerializer.SerializeObject(this);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }

    //checks if all the instance variables in a User instance are valid
    class UserValidator:AbstractValidator<User>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        string PasswordCheck;

        public UserValidator(String pwCheck)
        {
            this.PasswordCheck = pwCheck;

            //all the validation rules
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Please specify a user name");
            RuleFor(user => user.EMail).Must(BeAValidEmail).WithMessage("Please specify an email adress");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Please specify a password").Equal(pwCheck).WithMessage("You entered two different passwords");
        }

        private bool BeAValidEmail(String email)
        {
            return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
