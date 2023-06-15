using CarRentalSystem.Business.Model;

namespace CarRentalSystem.SampleConsoleApp.View;

internal interface IDeliveryRegistrationView : IView
{
    void RenderCarDeliveryRegistration(CarDelivery registeredDelivery);
    void ShowError(string message);
}