using System.Threading.Tasks;
using PokemonBetting.Client.Models;

namespace PokemonBetting.Client.Backend
{
    public interface IAuthProvider
    {
        /// <summary>
        /// Returns the authentication status of a current user.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Returns the authentication token of a current user.
        /// </summary>
        string AuthToken { get; }

        /// <summary>
        /// Asynchronously try to authenticate.
        /// </summary>
        /// <param name="userLogin">User login data.</param>
        Task<AuthResultEnum> TryAuth(UserLogin userLogin);
    }

    public enum AuthResultEnum
    {
        Ok,
        IncorrectCredentials,
        UnknownError
    }
}