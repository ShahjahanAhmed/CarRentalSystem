using CarRentalSystem.Business.Model;

namespace CarRentalSystem.SampleConsoleApp.View;

public interface IReturnRegistrationView
{
    void PromptPriceFactorsFromUser(CarReturn registeredCarReturn);
    void ShowSummary(RentalSummary rentalSummary);
}