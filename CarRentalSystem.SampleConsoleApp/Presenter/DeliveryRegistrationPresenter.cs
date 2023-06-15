using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;

namespace CarRentalSystem.SampleConsoleApp.Presenter
{
    public class DeliveryRegistrationPresenter
    {
        private readonly DeliveryRegistrationService deliveryRegistrationService;
        private readonly PriceByCategoryStrategyProvider priceByCategoryStrategyProvider;

        public DeliveryRegistrationPresenter(DeliveryRegistrationService deliveryRegistrationService, PriceByCategoryStrategyProvider priceByCategoryStrategyProvider)
        {
            this.deliveryRegistrationService = deliveryRegistrationService;
            this.priceByCategoryStrategyProvider = priceByCategoryStrategyProvider;
        }

        public CarDelivery RegisterDelivery(string carRegistrationNumber, string socialSecurityNumber, 
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            return deliveryRegistrationService.RegisterCarDelivery(carRegistrationNumber, socialSecurityNumber, carCategory, pickupTime, currentMeterReading);
        }

        public IReadOnlyCollection<string> SupportedCarCategories => priceByCategoryStrategyProvider.SupportedCarCategories;
    }
}
