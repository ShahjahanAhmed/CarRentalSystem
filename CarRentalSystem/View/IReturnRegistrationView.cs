using CarRentalSystem.Model;

namespace CarRentalSystem.View;

public interface IReturnRegistrationView
{
    void PromptPriceFactorsFromUser(CarReturn registeredCarReturn);
    void ShowSummary(RentalSummary rentalSummary);
}