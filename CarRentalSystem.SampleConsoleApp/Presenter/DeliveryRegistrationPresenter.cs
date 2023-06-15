using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;

namespace CarRentalSystem.SampleConsoleApp.Presenter
{
    internal class DeliveryRegistrationPresenter : IDeliveryRegistrationPresenter
    {
        private readonly IDeliveryRegistrationService deliveryRegistrationService;
        private readonly IPriceCalculationStrategyProvider priceCalculationStrategyProvider;

        public DeliveryRegistrationPresenter(IDeliveryRegistrationService deliveryRegistrationService, IPriceCalculationStrategyProvider priceCalculationStrategyProvider)
        {
            this.deliveryRegistrationService = deliveryRegistrationService;
            this.priceCalculationStrategyProvider = priceCalculationStrategyProvider;
        }

        public CarDelivery RegisterDelivery(string carRegistrationNumber, string socialSecurityNumber, 
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            return deliveryRegistrationService.RegisterCarDelivery(carRegistrationNumber, socialSecurityNumber, carCategory, pickupTime, currentMeterReading);
        }

        public IReadOnlyCollection<string> SupportedCarCategories => priceCalculationStrategyProvider.SupportedCarCategories;
    }
}
