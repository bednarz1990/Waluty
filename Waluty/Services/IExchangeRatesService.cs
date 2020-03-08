using System.Threading.Tasks;

namespace Waluty.Services
{
    public interface IExchangeRatesService
    {
        Task SaveRate();
    }
}