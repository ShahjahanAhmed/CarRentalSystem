using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Infrastructure
{
    public interface IAppIocContainer
    {
        DeliveryRegistrationView CreateDeliveryRegistrationView();
        MainView CreateMainView();
        ReturnRegistrationView CreateReturnRegistrationView();
    }
}