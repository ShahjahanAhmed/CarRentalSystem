namespace CarRentalSystem.Business.Model
{
    public class CarDelivery
    {
        public CarDelivery(string carCarRegistrationNumber)
        {
            CarRegistration = carCarRegistrationNumber ?? throw new ArgumentNullException(nameof(carCarRegistrationNumber));
        }

        public string CarCategory { get; set; }

        public long BookingNumber { get; protected set; } = -1;
        public string CarRegistration { get; set; }

        public string SocialSecurityNumber { get; set; }

        public DateTime PickupTime { get; set; }

        public int MeterReadingAtDelivery { get; set; }

        public CarDelivery DeepCopy()
        {
            var clone = (CarDelivery)MemberwiseClone();
            clone.PickupTime = new DateTime(PickupTime.Ticks);
            return clone;
        }
    }
}
