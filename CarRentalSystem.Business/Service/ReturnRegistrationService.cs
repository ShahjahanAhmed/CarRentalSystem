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
            if (bookingNumber <= 0)
            {
                throw new ArgumentException("Booking number must be greater than 0.");
            }

            var carRental = deliveryRepository.Get(bookingNumber);

            if (carRental == null)
            {
                throw new EntryNotFoundException($"Rental with booking number {bookingNumber} not found.");
            }

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
    }
}
