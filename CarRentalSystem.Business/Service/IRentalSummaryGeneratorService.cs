using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service;

public interface IRentalSummaryGeneratorService
{
    RentalSummary GenerateSummary(long bookingNumber, int baseDayRental, int baseKmPrice);
}