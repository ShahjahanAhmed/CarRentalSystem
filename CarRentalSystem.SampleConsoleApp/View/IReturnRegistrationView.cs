using CarRentalSystem.Business.Model;

namespace CarRentalSystem.SampleConsoleApp.View;

internal interface IReturnRegistrationView : IView
{
    void PromptPriceFactorsFromUser(CarReturn registeredCarReturn);
    void ShowSummary(RentalSummary rentalSummary);
    void ShowError(string message);
}