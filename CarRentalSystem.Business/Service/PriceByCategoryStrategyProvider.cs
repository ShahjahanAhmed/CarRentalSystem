using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service
{
    internal class PriceByCategoryStrategyProvider : IPriceCalculationStrategyProvider
    {
        private readonly Dictionary<string, CarPriceStrategy> priceStrategies = new();

        public PriceByCategoryStrategyProvider()
        {
            RegisterStrategy("Small car", factors => factors.BaseDayRental * factors.NumberOfDays);
            RegisterStrategy("Combi", factors => factors.BaseDayRental * factors.NumberOfDays * 1.3 + factors.BaseKmPrice * factors.NumberOfKm);
            RegisterStrategy("Truck", factors => factors.BaseDayRental * factors.NumberOfDays * 1.5 + factors.BaseKmPrice * factors.NumberOfKm * 1.5);
        }

        private void RegisterStrategy(string categoryName, Func<PriceFactors, double> priceStrategy)
        {
            priceStrategies.Add(categoryName, new CarPriceStrategy(priceStrategy));
        }

        public IReadOnlyCollection<string> SupportedCarCategories => priceStrategies.Keys.ToList().AsReadOnly();

        public CarPriceStrategy? GetPriceStrategy(string categoryName)
        {
            return priceStrategies.TryGetValue(categoryName, out var value) ? value : null;
        }
    }
}
