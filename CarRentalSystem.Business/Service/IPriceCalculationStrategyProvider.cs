using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service;

/// <summary>
/// Provides contracts for price calculation strategy based on car category.
/// </summary>
public interface IPriceCalculationStrategyProvider
{
    IReadOnlyCollection<string> SupportedCarCategories { get; }

    CarPriceStrategy? GetPriceStrategy(string categoryName);
}