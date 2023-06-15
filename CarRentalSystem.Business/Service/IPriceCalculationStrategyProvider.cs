using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service;

public interface IPriceCalculationStrategyProvider
{
    IReadOnlyCollection<string> SupportedCarCategories { get; }

    CarPriceStrategy? GetPriceStrategy(string categoryName);
}