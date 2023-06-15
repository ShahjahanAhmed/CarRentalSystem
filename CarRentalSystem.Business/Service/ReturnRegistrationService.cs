using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;
using NLog;

namespace CarRentalSystem.Business.Service
{
    internal class ReturnRegistrationService : IReturnRegistrationService
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IReturnRepository returnRepository;

        public ReturnRegistrationService(IDeliveryRepository deliveryRepository, IReturnRepository returnRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.returnRepository = returnRepository;
        }

        public CarReturn RegisterReturnOfCar(long bookingNumber, DateTime returnTime, int currentMeterReading)
        {
            ValidateBookingNumber(bookingNumber);

            var carDelivery = deliveryRepository.Get(bookingNumber);

            if (carDelivery == null)
            {
                throw new EntryNotFoundException($"Rental with booking number {bookingNumber} not found.");
            }

            ValidateReturnTime(carDelivery, returnTime);
            ValidateMeterReading(carDelivery, currentMeterReading);

            logger.Debug($"Registering car return with booking number {bookingNumber}...");

            var newCarReturn = new CarReturn(bookingNumber)
            {
                ReturnTime = returnTime,
                MeterReadingAtReturn = currentMeterReading
            };

            var registerReturnOfCar = returnRepository.Create(newCarReturn);
            logger.Debug($"Car return registration with booking number {bookingNumber} is done.");

            return registerReturnOfCar;
        }

        private static void ValidateBookingNumber(long bookingNumber)
        {
            if (bookingNumber <= 0)
            {
                throw new InvalidInputDataException("Booking number must be greater than 0.");
            }
        }

        private static void ValidateMeterReading(CarDelivery carDelivery, int meterReadingAtReturn)
        {
            if (carDelivery.MeterReadingAtDelivery >= meterReadingAtReturn)
            {
                throw new InvalidInputDataException("Meter reading at the time of return must be greater than the reading at pick up time.");
            }
        }

        private static void ValidateReturnTime(CarDelivery carDelivery, DateTime returnTime)
        {
            if (carDelivery.PickupTime >= returnTime)
            {
                throw new InvalidInputDataException("Return time must be later than pick up time.");
            }
        }
    }
}
