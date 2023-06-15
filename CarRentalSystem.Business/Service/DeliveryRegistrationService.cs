using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;

namespace CarRentalSystem.Business.Service
{
    public class DeliveryRegistrationService
    {
        private readonly PriceByCategoryStrategyProvider priceByCategoryStrategyProvider;
        private readonly IDeliveryRepository deliveryRepository;

        public DeliveryRegistrationService(PriceByCategoryStrategyProvider priceByCategoryStrategyProvider, IDeliveryRepository deliveryRepository)
        {
            this.priceByCategoryStrategyProvider = priceByCategoryStrategyProvider;
            this.deliveryRepository = deliveryRepository;
        }

        public CarDelivery RegisterCarDelivery(string carRegistrationNumber, string socialSecurityNumber,
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            var carPriceStrategy = priceByCategoryStrategyProvider.GetPriceStrategy(carCategory);
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

            var priceStrategy = priceByCategoryStrategyProvider.GetPriceStrategy(carCategory);
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
