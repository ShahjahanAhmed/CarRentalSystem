using CarRentalSystem.Model;
using CarRentalSystem.Service;

namespace CarRentalSystem.Presenter
{
    public class DeliveryRegistrationPresenter
    {
        private readonly DeliveryRegistrationService deliveryRegistrationService;

        public DeliveryRegistrationPresenter(DeliveryRegistrationService deliveryRegistrationService)
        {
            this.deliveryRegistrationService = deliveryRegistrationService;
        }

        public CarDelivery RegisterDelivery(string carRegistrationNumber, string socialSecurityNumber, 
            string carCategory, DateTime pickupTime, int currentMeterReading)
        {
            return deliveryRegistrationService.RegisterCarDelivery(carRegistrationNumber, socialSecurityNumber, carCategory, pickupTime, currentMeterReading);
        }
    }
}
