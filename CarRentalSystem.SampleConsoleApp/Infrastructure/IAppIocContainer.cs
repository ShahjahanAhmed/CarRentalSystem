using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Infrastructure
{
    internal interface IAppIocContainer
    {
        IView CreateDeliveryRegistrationView();
        IView CreateMainView();
        IReturnRegistrationView CreateReturnRegistrationView();
    }
}