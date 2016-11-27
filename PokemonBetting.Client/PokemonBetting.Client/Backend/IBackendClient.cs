using System.Threading.Tasks;
using PokemonBetting.Client.Backend.CallResults;
using PokemonBetting.Client.Models;

namespace PokemonBetting.Client.Backend
{
    public interface IBackendClient
    {
        Task<LoginCallResult> Login(UserLogin userLogin);

        Task<CreateUserCallResult> CreateUser(NewUser userData);

        Task<GetAuthenticatedUserCallResult> GetAuthenticatedUser();

        Task<DepositCallResult> Deposit(int amount);

        Task<WithdrawCallResult> Withdraw(int amount);
    }
}