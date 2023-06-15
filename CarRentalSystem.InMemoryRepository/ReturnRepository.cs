using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;

namespace CarRentalSystem.InMemoryRepository
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly Dictionary<long, CarReturn> dataStore = new();

        public CarReturn? Get(long bookingNumber)
        {
            dataStore.TryGetValue(bookingNumber, out var value);

            CarReturn? carReturn = null;
            if (value != null)
            {
                carReturn = value.DeepCopy();
            }
            return carReturn;
        }

        public CarReturn Create(CarReturn newCarReturn)
        {
            var copyOfNewCarReturn = newCarReturn.DeepCopy();
            dataStore[newCarReturn.BookingNumber] = copyOfNewCarReturn;
            return copyOfNewCarReturn.DeepCopy();
        }

        public void Update(CarReturn updatedCarReturn)
        {
            dataStore[updatedCarReturn.BookingNumber] = updatedCarReturn.DeepCopy();
        }

        public void Delete(long bookingNumber)
        {
            dataStore.Remove(bookingNumber);
        }
    }
}
