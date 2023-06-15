using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;
using CarRentalSystem.Business.Utils;

namespace CarRentalSystem.InMemoryRepository
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly BookingNumberGenerator bookingNumberGenerator;

        public DeliveryRepository(BookingNumberGenerator bookingNumberGenerator)
        {
            this.bookingNumberGenerator = bookingNumberGenerator;
        }

        private readonly Dictionary<long, CarDelivery> dataStore = new();

        public CarDelivery? Get(long bookingNumber)
        {
            dataStore.TryGetValue(bookingNumber, out var value);

            CarDelivery? carRental = null;
            if (value != null)
            {
                carRental = value.DeepCopy();
            }
            return carRental;
        }

        public CarDelivery Create(CarDelivery newCarDelivery)
        {
            var carDelivery = CreateInternalCarDelivery(newCarDelivery, bookingNumberGenerator.GetNewBookingNumber());
            dataStore[carDelivery.BookingNumber] = carDelivery;
            return carDelivery;
        }

        public void Update(CarDelivery carRental)
        {
            dataStore[carRental.BookingNumber] = CreateInternalCarDelivery(carRental, carRental.BookingNumber);
        }

        public void Delete(long bookingNumber)
        {
            dataStore.Remove(bookingNumber);
        }

        private static CarDelivery CreateInternalCarDelivery(CarDelivery carRental, long bookingNumber)
        {
            return new InternalCarDelivery(carRental.CarRegistration,
                bookingNumber)
            {
                CarCategory = carRental.CarCategory,
                MeterReadingAtDelivery = carRental.MeterReadingAtDelivery,
                PickupTime = carRental.PickupTime,
                SocialSecurityNumber = carRental.SocialSecurityNumber
            };
        }

        private class InternalCarDelivery : CarDelivery
        {
            public InternalCarDelivery(string carCarRegistrationNumber, long bookingNumber)
                : base(carCarRegistrationNumber)
            {
                BookingNumber = bookingNumber;
            }
        }
    }


}
