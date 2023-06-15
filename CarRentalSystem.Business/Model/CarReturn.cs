namespace CarRentalSystem.Business.Model;

public class CarReturn
{
    public CarReturn(long bookingNumber)
    {
        BookingNumber = bookingNumber;
    }

    public long BookingNumber { get; }

    public DateTime ReturnTime { get; set; }

    public int MeterReadingAtReturn { get; set; }

    public CarReturn DeepCopy()
    {
        var clone = (CarReturn)MemberwiseClone();
        clone.ReturnTime = new DateTime(ReturnTime.Ticks);
        return clone;
    }
}