namespace CarRentalSystem.Business.Model
{
    public class RentalSummary
    {
        public long BookingNumber { get; }
        public double TotalPrice { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfKm { get; set; }

        public RentalSummary(long bookingNumber)
        {
            BookingNumber = bookingNumber;
        }
    }
}
