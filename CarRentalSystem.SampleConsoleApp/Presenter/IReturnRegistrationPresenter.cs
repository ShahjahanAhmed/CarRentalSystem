using CarRentalSystem.Business.Model;
using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Presenter;

internal interface IReturnRegistrationPresenter
{
    void Bind(IReturnRegistrationView returnRegistrationView);
    void RegisterReturnOfCar(long bookingNumber, DateTime returnTime, int currentMeterReading);
    void OnPriceFactorsCollected(CarReturn registeredCarReturn, int baseDayRental, int baseKmPrice);
}