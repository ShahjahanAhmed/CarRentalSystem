using CarRentalSystem.Business.Model;

namespace CarRentalSystem.SampleConsoleApp.Presenter;

internal interface IDeliveryRegistrationPresenter
{
    CarDelivery RegisterDelivery(string carRegistrationNumber, string socialSecurityNumber, 
        string carCategory, DateTime pickupTime, int currentMeterReading);

    IReadOnlyCollection<string> SupportedCarCategories { get; }
}