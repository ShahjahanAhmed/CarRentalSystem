using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Service;

public interface IReturnRegistrationService
{
    CarReturn RegisterReturnOfCar(long bookingNumber, DateTime returnTime, int currentMeterReading);
}