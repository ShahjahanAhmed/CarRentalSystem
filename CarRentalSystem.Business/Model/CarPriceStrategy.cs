namespace CarRentalSystem.Business.Model
{
    /// <summary>
    /// Strategy for calculating price for the rental.
    /// </summary>
    public class CarPriceStrategy
    {
        private readonly Func<PriceFactors, double> priceCalculationStrategy;

        public CarPriceStrategy(Func<PriceFactors, double> priceCalculationStrategy)
        {
            this.priceCalculationStrategy = priceCalculationStrategy;
        }

        public double CalculatePrice(PriceFactors factors)
        {
            return priceCalculationStrategy(factors);
        }
    }
}
