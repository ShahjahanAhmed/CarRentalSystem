using CarRentalSystem.Business.Infrastructure;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Service;
using CarRentalSystem.SampleConsoleApp.Presenter;
using CarRentalSystem.SampleConsoleApp.View;

namespace CarRentalSystem.SampleConsoleApp.Infrastructure
{
    public class AppIocContainer : IocContainer, IAppIocContainer
    {
        public DeliveryRegistrationView CreateDeliveryRegistrationView()
        {
            return new DeliveryRegistrationView(CreateDeliveryRegistrationPresenter());
        }

        public ReturnRegistrationView CreateReturnRegistrationView()
        {
            return new ReturnRegistrationView(CreateReturnRegistrationPresenter());
        }

        private DeliveryRegistrationPresenter CreateDeliveryRegistrationPresenter()
        {
            var deliveryRegistrationService = GetSingletonInstance<DeliveryRegistrationService>();
            var priceByCategoryStrategyProvider = GetSingletonInstance<PriceByCategoryStrategyProvider>();
            return new DeliveryRegistrationPresenter(deliveryRegistrationService, priceByCategoryStrategyProvider);
        }

        private ReturnRegistrationPresenter CreateReturnRegistrationPresenter()
        {
            var returnRegistrationService = GetSingletonInstance<ReturnRegistrationService>();
            var rentalPriceCalculationService = GetSingletonInstance<RentalSummaryGeneratorService>();

            return new ReturnRegistrationPresenter(returnRegistrationService, rentalPriceCalculationService);
        }

        public MainView CreateMainView()
        {
            return new MainView(this);
        }
    }
}
