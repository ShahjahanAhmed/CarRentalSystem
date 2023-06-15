using CarRentalSystem.Model;
using CarRentalSystem.Service;
using CarRentalSystem.View;

namespace CarRentalSystem.Presenter
{
    public class ReturnRegistrationPresenter
    {
        private readonly ReturnRegistrationService returnRegistrationService;
        private readonly RentalSummaryGeneratorService rentalSummaryGeneratorService;
        private IReturnRegistrationView view;

        public ReturnRegistrationPresenter(ReturnRegistrationService returnRegistrationService, RentalSummaryGeneratorService rentalSummaryGeneratorService)
        {
            this.returnRegistrationService = returnRegistrationService;
            this.rentalSummaryGeneratorService = rentalSummaryGeneratorService;
        }

        public void Bind(IReturnRegistrationView returnRegistrationView)
        {
            view = returnRegistrationView;
        }

        public void RegisterReturnOfCar(long bookingNumber, DateTime returnTime, int currentMeterReading)
        {
            var registeredCarReturn = returnRegistrationService.RegisterReturnOfCar(bookingNumber, returnTime, currentMeterReading);
            view.PromptPriceFactorsFromUser(registeredCarReturn);
        }


        public void OnPriceFactorsCollected(CarReturn registeredCarReturn, int baseDayRental, int baseKmPrice)
        {
            var rentalSummary = rentalSummaryGeneratorService.GenerateSummary(registeredCarReturn.BookingNumber, baseDayRental, baseKmPrice);
            view.ShowSummary(rentalSummary);
        }
    }
}
