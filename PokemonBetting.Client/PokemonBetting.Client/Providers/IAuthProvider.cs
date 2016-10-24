using System.Threading.Tasks;

namespace PokemonBetting.Client.Providers
{
    public interface IAuthProvider
    {
        /// <summary>
        /// Returns the authentication status of a current user.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Asynchronously try to authenticate.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        Task TryAuth(string username, string password);
    }
}