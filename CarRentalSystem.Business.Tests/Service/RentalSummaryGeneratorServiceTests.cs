using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;
using CarRentalSystem.Business.Service;
using Moq;

namespace CarRentalSystem.Business.Tests.Service
{
    internal class RentalSummaryGeneratorServiceTests
    {
        private readonly RentalSummaryGeneratorService rentalSummaryGeneratorService;
        private readonly Mock<IDeliveryRepository> deliveryRepositoryMock;
        private readonly Mock<IReturnRepository> returnRepositoryMock;
        private readonly Mock<IPriceCalculationStrategyProvider> priceCalculationStrategyMock;

        public RentalSummaryGeneratorServiceTests()
        {
            deliveryRepositoryMock = new Mock<IDeliveryRepository>();
            returnRepositoryMock = new Mock<IReturnRepository>();
            priceCalculationStrategyMock = new Mock<IPriceCalculationStrategyProvider>();
            rentalSummaryGeneratorService = new RentalSummaryGeneratorService(deliveryRepositoryMock.Object, returnRepositoryMock.Object, priceCalculationStrategyMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            deliveryRepositoryMock.Reset();
        }

        [Test]
        public void TestSummaryGeneration_WhenACarDeliveryAndReturnRegistrationExists_ShouldGenerateSummarySuccesfully()
        {
            const int bookingNumber = 1;
            const int baseRentalDay = 100;
            const int baseKmPrice = 2;
            const string carCategory = "Truck";

            var carDelivery = SetupCarDelivery(carCategory);
            var carReturn = SetupCarReturn(bookingNumber);
            var carPriceStrategy = SetupCarPriceStrategy(3);

            deliveryRepositoryMock.Setup(repository => repository.Get(bookingNumber)).Returns(carDelivery);
            returnRepositoryMock.Setup(repository => repository.Get(bookingNumber)).Returns(carReturn);

            priceCalculationStrategyMock
                .Setup(provider => provider.GetPriceStrategy(carCategory))
                .Returns(carPriceStrategy);

            var rentalSummary = rentalSummaryGeneratorService.GenerateSummary(bookingNumber, baseRentalDay, baseKmPrice);
            Assert.That(rentalSummary.BookingNumber, Is.EqualTo(bookingNumber));
            Assert.That(rentalSummary.NumberOfDays, Is.EqualTo(2));
            Assert.That(rentalSummary.NumberOfKm, Is.EqualTo(100));
            Assert.That(rentalSummary.TotalPrice, Is.EqualTo(1200d));
        }

        private static CarPriceStrategy SetupCarPriceStrategy(double factor)
        {
            var carPriceStrategy = new CarPriceStrategy(factors =>
                factors.BaseDayRental * factors.NumberOfDays * factor +
                factors.BaseKmPrice * factors.NumberOfKm * factor);
            return carPriceStrategy;
        }

        private static CarReturn SetupCarReturn(int bookingNumber)
        {
            var carReturn = new CarReturn(bookingNumber)
            {
                MeterReadingAtReturn = 200,
                ReturnTime = DateTime.Now + TimeSpan.FromDays(2) + TimeSpan.FromMinutes(10)
            };
            return carReturn;
        }

        private static CarDelivery SetupCarDelivery(string carCategory)
        {
            var carDelivery = new CarDelivery("BDF234")
            {
                CarCategory = carCategory,
                PickupTime = DateTime.Now + TimeSpan.FromMinutes(10),
                MeterReadingAtDelivery = 100,
                SocialSecurityNumber = "19891212-4123"
            };
            return carDelivery;
        }
    }
}
