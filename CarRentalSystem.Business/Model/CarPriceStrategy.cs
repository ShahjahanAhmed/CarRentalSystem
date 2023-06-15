namespace CarRentalSystem.Business.Model
{
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
