using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service;

/// <summary>
/// Provides contracts to generate summary of a car rental upon both car delivery and car return are done.
/// </summary>
public interface IRentalSummaryGeneratorService
{
    RentalSummary GenerateSummary(long bookingNumber, int baseDayRental, int baseKmPrice);
}