using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;

namespace CarRentalSystem.Business.Service
{
    internal class DeliveryRegistrationService : IDeliveryRegistrationService
    {
        private readonly IPriceCalculationStrategyProvider priceCalculationStrategyProvider;
        private readonly IDeliveryRepository deliveryRepository;

        public DeliveryRegistrationService(IPriceCalculationStrategyProvider priceCalculationStrategyProvider, IDeliveryRepository deliveryRepository)
        {
            this.priceCalculationStrategyProvider = priceCalculationStrategyProvider;
            this.deliveryRepository = deliveryRepository;
        }

        public CarDelivery RegisterCarDelivery(string carRegistrationNumber, string socialSecurityNumber,
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            var carPriceStrategy = priceCalculationStrategyProvider.GetPriceStrategy(carCategory);
            if (carPriceStrategy == null)
            {
                throw new CategoryNotSupportedException(carCategory);
            }

            var newCarDelivery = new CarDelivery(carRegistrationNumber)
            {
                SocialSecurityNumber = socialSecurityNumber,
                CarCategory = carCategory,
                PickupTime = pickupTime,
                MeterReadingAtDelivery = currentMeterReading,
            };

            return deliveryRepository.Create(newCarDelivery); ;
        }

        public void UpdateCarDelivery(long bookingNumber, string carRegistrationNumber, string socialSecurityNumber,
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            var carDelivery = deliveryRepository.Get(bookingNumber);
            if (carDelivery == null)
            {
                throw new EntryNotFoundException($"Car delivery registration with booking number {bookingNumber} not found.");
            }

            var priceStrategy = priceCalculationStrategyProvider.GetPriceStrategy(carCategory);
            if (priceStrategy == null)
            {
                throw new CategoryNotSupportedException(carCategory);
            }

            carDelivery.CarCategory = carCategory;
            carDelivery.CarRegistration = carRegistrationNumber;
            carDelivery.MeterReadingAtDelivery = currentMeterReading;
            carDelivery.PickupTime = pickupTime;
            carDelivery.SocialSecurityNumber = socialSecurityNumber;

            deliveryRepository.Update(carDelivery);
        }
    }
}
