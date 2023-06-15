using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;
using NLog;

namespace CarRentalSystem.Business.Service
{
    internal class DeliveryRegistrationService : IDeliveryRegistrationService
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
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

            ValidatePickupTime(pickupTime);

            logger.Debug($"Registering delivery with registration number {carRegistrationNumber}");
            var newCarDelivery = new CarDelivery(carRegistrationNumber)
            {
                SocialSecurityNumber = socialSecurityNumber,
                CarCategory = carCategory,
                PickupTime = pickupTime,
                MeterReadingAtDelivery = currentMeterReading,
            };

            var registerCarDelivery = deliveryRepository.Create(newCarDelivery);
            logger.Debug($"Registartion of delivery done with booking number {registerCarDelivery.BookingNumber}");

            return registerCarDelivery; ;
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

            ValidatePickupTime(pickupTime);

            logger.Debug($"Update delivery registration with booking number {bookingNumber}");

            carDelivery.CarCategory = carCategory;
            carDelivery.CarRegistration = carRegistrationNumber;
            carDelivery.MeterReadingAtDelivery = currentMeterReading;
            carDelivery.PickupTime = pickupTime;
            carDelivery.SocialSecurityNumber = socialSecurityNumber;

            deliveryRepository.Update(carDelivery);

            logger.Debug($"Updating delivery registration with booking number {bookingNumber} is done.");
        }

        private static void ValidatePickupTime(DateTime pickupTime)
        {
            if (pickupTime < DateTime.Now)
            {
                throw new InvalidInputDataException("Pick time must be later than the current time.");
            }
        }
    }
}
