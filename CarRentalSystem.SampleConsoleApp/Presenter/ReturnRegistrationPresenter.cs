using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Presenter
{
    internal class ReturnRegistrationPresenter : IReturnRegistrationPresenter
    {
        private readonly IReturnRegistrationService returnRegistrationService;
        private readonly IRentalSummaryGeneratorService rentalSummaryGeneratorService;
        private IReturnRegistrationView view;

        public ReturnRegistrationPresenter(IReturnRegistrationService returnRegistrationService, IRentalSummaryGeneratorService rentalSummaryGeneratorService)
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
