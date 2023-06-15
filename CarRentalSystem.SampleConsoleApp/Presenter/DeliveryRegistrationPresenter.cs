using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.View;
using NLog;

namespace CarRentalSystem.SampleConsoleApp.Presenter
{
    /// <summary>
    /// Provides presentation logic for car delivery registration.
    /// </summary>
    internal class DeliveryRegistrationPresenter : IDeliveryRegistrationPresenter
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IDeliveryRegistrationService deliveryRegistrationService;
        private readonly IPriceCalculationStrategyProvider priceCalculationStrategyProvider;
        private IDeliveryRegistrationView view;

        public DeliveryRegistrationPresenter(IDeliveryRegistrationService deliveryRegistrationService, IPriceCalculationStrategyProvider priceCalculationStrategyProvider)
        {
            this.deliveryRegistrationService = deliveryRegistrationService;
            this.priceCalculationStrategyProvider = priceCalculationStrategyProvider;
        }

        public void Bind(IDeliveryRegistrationView deliveryRegistrationView)
        {
            this.view = deliveryRegistrationView;
        }

        public void RegisterDelivery(string carRegistrationNumber, string socialSecurityNumber, 
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            CarDelivery registeredCarDelivery = null;
            try
            { 
                registeredCarDelivery = deliveryRegistrationService.RegisterCarDelivery(carRegistrationNumber, socialSecurityNumber, carCategory, pickupTime, currentMeterReading);

            }
            catch (Exception e)
            {
                logger.Error($"Failed to register car delivery. {e.Message}");
                view.ShowError($"Car delivery registration failed. {e.Message}");
            }

            if (registeredCarDelivery != null)
            {
                logger.Info($"Successfully registered car delivery with booking number {registeredCarDelivery.BookingNumber}");
                view.RenderCarDeliveryRegistration(registeredCarDelivery);
            }
        }

        public IReadOnlyCollection<string> SupportedCarCategories => priceCalculationStrategyProvider.SupportedCarCategories;
    }
}
