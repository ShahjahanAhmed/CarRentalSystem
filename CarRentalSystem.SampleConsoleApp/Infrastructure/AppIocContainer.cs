using CarRentalSystem.Business.Infrastructure;
using CarRentalSystem.Business.Repository;
using CarRentalSystem.Business.Service;
using CarRentalSystem.Business.Utils;
using CarRentalSystem.InMemoryRepository;
using CarRentalSystem.SampleConsoleApp.Presenter;
using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Infrastructure
{
    internal class AppIocContainer : IocContainerBase, IAppIocContainer
    {
        public AppIocContainer()
        {
            RegisterSingleton(CreateDeliveryRepository());
            RegisterSingleton<IReturnRepository>(new ReturnRepository());
            RegisterSingletonServices();
        }

        private IDeliveryRepository CreateDeliveryRepository()
        {
            var bookingNumberGenerator = GetSingletonInstance<BookingNumberGenerator>();
            return new DeliveryRepository(bookingNumberGenerator);
        }

        public IView CreateDeliveryRegistrationView()
        {
            return new DeliveryRegistrationView(CreateDeliveryRegistrationPresenter());
        }

        public IReturnRegistrationView CreateReturnRegistrationView()
        {
            return new ReturnRegistrationView(CreateReturnRegistrationPresenter());
        }

        private IDeliveryRegistrationPresenter CreateDeliveryRegistrationPresenter()
        {
            var deliveryRegistrationService = GetSingletonInstance<IDeliveryRegistrationService>();
            var priceByCategoryStrategyProvider = GetSingletonInstance<IPriceCalculationStrategyProvider>();
            return new DeliveryRegistrationPresenter(deliveryRegistrationService, priceByCategoryStrategyProvider);
        }

        private IReturnRegistrationPresenter CreateReturnRegistrationPresenter()
        {
            var returnRegistrationService = GetSingletonInstance<IReturnRegistrationService>();
            var rentalPriceCalculationService = GetSingletonInstance<IRentalSummaryGeneratorService>();

            return new ReturnRegistrationPresenter(returnRegistrationService, rentalPriceCalculationService);
        }

        public IView CreateMainView()
        {
            return new MainView(this);
        }
    }
}
