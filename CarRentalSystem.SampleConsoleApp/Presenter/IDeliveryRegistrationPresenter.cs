using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Presenter;

internal interface IDeliveryRegistrationPresenter
{
    void RegisterDelivery(string carRegistrationNumber, string socialSecurityNumber, 
        string carCategory, DateTime pickupTime, int currentMeterReading);

    IReadOnlyCollection<string> SupportedCarCategories { get; }
    void Bind(IDeliveryRegistrationView view);
}