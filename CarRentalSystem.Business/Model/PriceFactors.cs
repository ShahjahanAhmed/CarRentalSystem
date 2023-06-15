namespace CarRentalSystem.Business.Model
{
    public class PriceFactors
    {
        public PriceFactors(int baseDayRental, int baseKmPrice, int numberOfDays, int numberOfKm)
        {
            NumberOfDays = numberOfDays;
            NumberOfKm = numberOfKm;
            BaseDayRental = baseDayRental;
            BaseKmPrice = baseKmPrice;
        }

        public int NumberOfDays { get; }
        public int NumberOfKm { get; }
        public int BaseDayRental { get;  }

        public int BaseKmPrice { get;  }
    }
}
