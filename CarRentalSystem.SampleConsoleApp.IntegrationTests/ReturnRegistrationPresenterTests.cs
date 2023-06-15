using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.Infrastructure;
using CarRentalSystem.SampleConsoleApp.Presenter;
using CarRentalSystem.SampleConsoleApp.View;
using Moq;

namespace CarRentalSystem.SampleConsoleApp.IntegrationTests
{
    [TestFixture]
    internal class ReturnRegistrationPresenterTests
    {
        private readonly ReturnRegistrationPresenter returnRegistrationPresenter;
        private readonly Mock<IReturnRegistrationView> returnRegistrationViewMock;
        private readonly AppIocContainer appIocContainer;

        public ReturnRegistrationPresenterTests()
        {
            returnRegistrationViewMock = new Mock<IReturnRegistrationView>();
            appIocContainer = new AppIocContainer();
            var returnRegistrationService = appIocContainer.GetSingletonInstance<IReturnRegistrationService>();
            var rentalSummaryGeneratorService = appIocContainer.GetSingletonInstance<IRentalSummaryGeneratorService>();
            returnRegistrationPresenter = new ReturnRegistrationPresenter(returnRegistrationService, rentalSummaryGeneratorService);
            returnRegistrationPresenter.Bind(returnRegistrationViewMock.Object);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            const string socialSecurityNumber = "19891212-4123";
            const string carRegistrationNumber = "BDF342";
            const int meterReadingAtDelivery = 100;
            var pickupTime = DateTime.Now + TimeSpan.FromMinutes(10);

            var deliveryRegistrationViewMock = new Mock<IDeliveryRegistrationView>();
            var deliveryRegistrationService = appIocContainer.GetSingletonInstance<IDeliveryRegistrationService>();
            var priceCalculationStrategyProvider = appIocContainer.GetSingletonInstance<IPriceCalculationStrategyProvider>();
            var deliveryRegistrationPresenter = new DeliveryRegistrationPresenter(deliveryRegistrationService, priceCalculationStrategyProvider);
            deliveryRegistrationPresenter.Bind(deliveryRegistrationViewMock.Object);

            deliveryRegistrationPresenter.RegisterDelivery(carRegistrationNumber, socialSecurityNumber, "Small car", pickupTime, meterReadingAtDelivery);
            deliveryRegistrationPresenter.RegisterDelivery(carRegistrationNumber, socialSecurityNumber, "Combi", pickupTime, meterReadingAtDelivery);
            deliveryRegistrationPresenter.RegisterDelivery(carRegistrationNumber, socialSecurityNumber, "Truck", pickupTime, meterReadingAtDelivery);
        }

        [TestCase(1, 200d)]
        [TestCase(2, 460d)]
        [TestCase(3, 600d)]
        public void TestSummaryGeneration_WhenCarDeliveryRegistrationsExistsForDifferentCarCategory_SummaryShouldHaveExpectedData(long expectedBookingNumber, double expectedTotalPrice)
        {
            const int baseDayRental = 100;
            const int baseKmPrice = 2;
            const int meterReadingAtReturn = 200;
            var expectedReturnTime = DateTime.Now + TimeSpan.FromDays(2) + TimeSpan.FromMinutes(10); ;

            CarReturn? registeredCarReturn = null;
            RentalSummary? rentalSummary = null;

            returnRegistrationViewMock.Setup(view => view.PromptPriceFactorsFromUser(It.IsAny<CarReturn>()))
                .Callback((CarReturn carReturn) => registeredCarReturn = carReturn);
            returnRegistrationViewMock.Setup(view => view.ShowSummary(It.IsAny<RentalSummary>()))
                .Callback((RentalSummary summary) => rentalSummary = summary);

            returnRegistrationPresenter.RegisterReturnOfCar(expectedBookingNumber, expectedReturnTime, meterReadingAtReturn);

            Assert.That(registeredCarReturn.BookingNumber, Is.EqualTo(expectedBookingNumber));
            Assert.That(registeredCarReturn.MeterReadingAtReturn, Is.EqualTo(meterReadingAtReturn));
            Assert.That(registeredCarReturn.ReturnTime, Is.EqualTo(expectedReturnTime));

            returnRegistrationPresenter.OnPriceFactorsCollected(registeredCarReturn, baseDayRental, baseKmPrice);

            Assert.That(rentalSummary.BookingNumber, Is.EqualTo(expectedBookingNumber));
            Assert.That(rentalSummary.NumberOfDays, Is.EqualTo(2));
            Assert.That(rentalSummary.NumberOfKm, Is.EqualTo(100));
            Assert.That(rentalSummary.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }
    }
}
