using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.View;
using NLog;

namespace CarRentalSystem.SampleConsoleApp.Presenter
{
    internal class ReturnRegistrationPresenter : IReturnRegistrationPresenter
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IReturnRegistrationService returnRegistrationService;
        private readonly IRentalSummaryGeneratorService rentalSummaryGeneratorService;
        private IReturnRegistrationView view;

        public ReturnRegistrationPresenter(IReturnRegistrationService returnRegistrationService,
            IRentalSummaryGeneratorService rentalSummaryGeneratorService)
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
            CarReturn registeredCarReturn = null;
            try
            {
                registeredCarReturn = returnRegistrationService.RegisterReturnOfCar(bookingNumber, returnTime, currentMeterReading);

            }
            catch (Exception e)
            {
                HandleError(e, $"Car return registration failed. {e.Message}");
            }

            if (registeredCarReturn != null)
            {
                view.PromptPriceFactorsFromUser(registeredCarReturn);
            }
        }

        private void HandleError(Exception e, string displayMessage)
        {
            logger.Error(e.Message);
            view.ShowError(displayMessage);
        }


        public void OnPriceFactorsCollected(CarReturn registeredCarReturn, int baseDayRental, int baseKmPrice)
        {
            RentalSummary rentalSummary = null;
            try
            {
                rentalSummary = rentalSummaryGeneratorService.GenerateSummary(registeredCarReturn.BookingNumber,
                    baseDayRental, baseKmPrice);

            }
            catch (Exception e)
            {
                HandleError(e, $"Rental summary generation failed. {e.Message}");
            }

            if (rentalSummary != null)
            {
                view.ShowSummary(rentalSummary);
            }
        }
    }
}
