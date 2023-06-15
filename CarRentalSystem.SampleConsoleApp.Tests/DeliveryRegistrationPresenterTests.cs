using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.Infrastructure;
using CarRentalSystem.SampleConsoleApp.Presenter;
using CarRentalSystem.SampleConsoleApp.View;
using Moq;

namespace CarRentalSystem.SampleConsoleApp.IntegrationTests
{
    internal class DeliveryRegistrationPresenterTests
    {
        private readonly DeliveryRegistrationPresenter deliveryRegistrationPresenter;
        private readonly Mock<IDeliveryRegistrationView> deliveryRegistrationViewMock;

        public DeliveryRegistrationPresenterTests()
        {
            deliveryRegistrationViewMock = new Mock<IDeliveryRegistrationView>();
            var appIocContainer = new AppIocContainer();
            var deliveryRegistrationService = appIocContainer.GetSingletonInstance<IDeliveryRegistrationService>();
            var priceCalculationStrategyProvider = appIocContainer.GetSingletonInstance<IPriceCalculationStrategyProvider>();
            deliveryRegistrationPresenter = new DeliveryRegistrationPresenter(deliveryRegistrationService, priceCalculationStrategyProvider);
            deliveryRegistrationPresenter.Bind(deliveryRegistrationViewMock.Object);
        }

        [Test]
        public void TestRegisterDelivery_WhenValidDataIsProvided_CarDeliveryEntryIsSavedWithBookingNumber()
        {
            const string socialSecurityNumber = "19891212-4123";
            const string carCategory = "Small car";
            const string carRegistrationNumber = "BDF342";
            const int currentMeterReading = 100;
            var pickupTime = DateTime.Now + TimeSpan.FromMinutes(10);

            CarDelivery registeredCarDelivery = null;

            deliveryRegistrationViewMock.Setup(view => view.RenderCarDeliveryRegistration(It.IsAny<CarDelivery>()))
                .Callback((CarDelivery carDelivery) => registeredCarDelivery = carDelivery);

            deliveryRegistrationPresenter.RegisterDelivery(carRegistrationNumber, socialSecurityNumber, carCategory,
                pickupTime, currentMeterReading);

            Assert.That(registeredCarDelivery.BookingNumber, Is.EqualTo(1));
            Assert.That(registeredCarDelivery.CarCategory, Is.EqualTo(carCategory));
            Assert.That(registeredCarDelivery.CarRegistration, Is.EqualTo(carRegistrationNumber));
            Assert.That(registeredCarDelivery.MeterReadingAtDelivery, Is.EqualTo(currentMeterReading));
            Assert.That(registeredCarDelivery.PickupTime, Is.EqualTo(pickupTime));
            Assert.That(registeredCarDelivery.SocialSecurityNumber, Is.EqualTo(socialSecurityNumber));
            Assert.That(registeredCarDelivery.SocialSecurityNumber, Is.EqualTo(socialSecurityNumber));
        }
    }
}
