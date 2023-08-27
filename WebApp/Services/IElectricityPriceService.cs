using WebApp.Dtos;

namespace WebApp.Services
{
    public interface IElectricityPriceService
    {
        Task<List<ElectricityPriceDto>> GetElectricityPrices();
    }
}