using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;

namespace CarRentalSystem.Business.Service
{
    public class RentalSummaryGeneratorService
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IReturnRepository returnRepository;
        private readonly PriceByCategoryStrategyProvider priceByCategoryStrategyProvider;

        public RentalSummaryGeneratorService(IDeliveryRepository deliveryRepository, IReturnRepository returnRepository,
            PriceByCategoryStrategyProvider priceByCategoryStrategyProvider)
        {
            this.deliveryRepository = deliveryRepository;
            this.returnRepository = returnRepository;
            this.priceByCategoryStrategyProvider = priceByCategoryStrategyProvider;
        }

        public RentalSummary GenerateSummary(long bookingNumber, int baseDayRental, int baseKmPrice)
        {
            if (bookingNumber <= 0)
            {
                throw new ArgumentException("Booking number must be greater than 0.");
            }

            var carDelivery = deliveryRepository.Get(bookingNumber);
            if (carDelivery == null)
            {
                throw new EntryNotFoundException($"Delivery registration with booking number {bookingNumber} not found.");
            }

            var carReturn = returnRepository.Get(bookingNumber);
            if (carReturn == null)
            {
                throw new EntryNotFoundException($"Return registration with booking number {bookingNumber} not found.");
            }

            var numberOfDays = CalculateNumberOfDays(carDelivery, carReturn);
            var numberOfKmDriven = CalculateNumberOfKm(carReturn, carDelivery);
            var totalPrice = CalculatePrice(carDelivery, carReturn, baseDayRental, baseKmPrice);

            return new RentalSummary(bookingNumber)
            {
                NumberOfDays = numberOfDays,
                NumberOfKm = numberOfKmDriven,
                TotalPrice = totalPrice
            };
        }

        private double CalculatePrice(CarDelivery carDelivery, CarReturn carReturn, int baseDayRental, int baseKmPrice)
        {
            var numberOfDays = CalculateNumberOfDays(carDelivery, carReturn);
            var numberOfKmDriven = CalculateNumberOfKm(carReturn, carDelivery);
            var priceFactors = new PriceFactors(baseDayRental, baseKmPrice, numberOfDays, numberOfKmDriven);

            var carPriceStrategy = priceByCategoryStrategyProvider.GetPriceStrategy(carDelivery.CarCategory);
            if (carPriceStrategy == null)
            {
                throw new CategoryNotSupportedException(carDelivery.CarCategory);
            }

            return carPriceStrategy.CalculatePrice(priceFactors);
        }

        private static int CalculateNumberOfKm(CarReturn carReturn, CarDelivery carDelivery)
        {
            return carReturn.MeterReadingAtReturn - carDelivery.MeterReadingAtDelivery;
        }

        private static int CalculateNumberOfDays(CarDelivery carDelivery, CarReturn carReturn)
        {
            var durationForRental = carReturn.ReturnTime - carDelivery.PickupTime;
            return durationForRental.Days;
        }
    }
}
