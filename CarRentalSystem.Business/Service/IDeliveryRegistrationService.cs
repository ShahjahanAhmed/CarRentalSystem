using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service;

public interface IDeliveryRegistrationService
{
    CarDelivery RegisterCarDelivery(string carRegistrationNumber, string socialSecurityNumber,
        string carCategory, DateTime pickupTime, int currentMeterReading);

    void UpdateCarDelivery(long bookingNumber, string carRegistrationNumber, string socialSecurityNumber,
        string carCategory, DateTime pickupTime, int currentMeterReading);
}