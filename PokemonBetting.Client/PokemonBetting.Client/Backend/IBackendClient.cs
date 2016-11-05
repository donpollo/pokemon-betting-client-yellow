using System.Threading.Tasks;
using PokemonBetting.Client.Backend.Models;
using PokemonBetting.Client.Models;

namespace PokemonBetting.Client.Backend
{
    public interface IBackendClient
    {
        Task<LoginCallResult> Login(UserLogin userLogin);
    }
}