using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.Infrastructure;
using CarRentalSystem.SampleConsoleApp.Presenter;

namespace CarRentalSystem.SampleConsoleApp.IntegrationTests
{
    internal class DeliveryRegistrationPresenterTests
    {
        private readonly DeliveryRegistrationPresenter deliveryRegistrationPresenter;

        public DeliveryRegistrationPresenterTests()
        {
            var appIocContainer = new AppIocContainer();
            var deliveryRegistrationService = appIocContainer.GetSingletonInstance<IDeliveryRegistrationService>();
            var priceCalculationStrategyProvider = appIocContainer.GetSingletonInstance<IPriceCalculationStrategyProvider>();
            deliveryRegistrationPresenter = new DeliveryRegistrationPresenter(deliveryRegistrationService, priceCalculationStrategyProvider);
        }

        [Test]
        public void TestRegisterDelivery_WhenValidDataIsProvided_CarDeliveryEntryIsSavedWithBookingNumber()
        {
            const string socialSecurityNumber = "19891212-4123";
            const string carCategory = "Small car";
            const string carRegistrationNumber = "BDF342";
            const int currentMeterReading = 100;
            var pickupTime = DateTime.Parse("2023-06-15T09:30");

            var carDelivery = deliveryRegistrationPresenter.RegisterDelivery(carRegistrationNumber, socialSecurityNumber, carCategory,
                pickupTime, currentMeterReading);

            Assert.That(carDelivery.BookingNumber, Is.EqualTo(1));
            Assert.That(carDelivery.CarCategory, Is.EqualTo(carCategory));
            Assert.That(carDelivery.CarRegistration, Is.EqualTo(carRegistrationNumber));
            Assert.That(carDelivery.MeterReadingAtDelivery, Is.EqualTo(currentMeterReading));
            Assert.That(carDelivery.PickupTime, Is.EqualTo(pickupTime));
            Assert.That(carDelivery.SocialSecurityNumber, Is.EqualTo(socialSecurityNumber));
            Assert.That(carDelivery.SocialSecurityNumber, Is.EqualTo(socialSecurityNumber));
        }
    }
}
