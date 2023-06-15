namespace CarRentalSystem.InMemoryRepository.Utils
{
    public class BookingNumberGenerator
    {
        private long currentBookingNumber = 0;

        public long GetNewBookingNumber()
        {
            return ++currentBookingNumber;
        }
    }
}
