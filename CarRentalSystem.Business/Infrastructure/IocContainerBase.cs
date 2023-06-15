using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Repository;
using CarRentalSystem.Business.Service;
using CarRentalSystem.Business.Utils;

namespace CarRentalSystem.Business.Infrastructure
{
    public abstract class IocContainerBase
    {
        private readonly Dictionary<Type, object> singletonInstances = new();

        protected IocContainerBase()
        {
            RegisterSingleton<IPriceCalculationStrategyProvider>(new PriceByCategoryStrategyProvider());
            RegisterSingleton(new BookingNumberGenerator());
        }

        protected void RegisterSingletonServices()
        {
            RegisterSingleton(CreateDeliveryRegistrationService());
            RegisterSingleton(CreateReturnRegistrationService());
            RegisterSingleton(CreateRentalPriceCalculationService());
        }

        private IRentalSummaryGeneratorService CreateRentalPriceCalculationService()
        {
            var deliveryRepository = GetSingletonInstance<IDeliveryRepository>();
            var returnRepository = GetSingletonInstance<IReturnRepository>();
            var priceByCategoryStrategyProvider = GetSingletonInstance<IPriceCalculationStrategyProvider>();
            return new RentalSummaryGeneratorService(deliveryRepository, returnRepository, priceByCategoryStrategyProvider);
        }

        private IReturnRegistrationService CreateReturnRegistrationService()
        {
            var deliveryRepository = GetSingletonInstance<IDeliveryRepository>();
            var returnRepository = GetSingletonInstance<IReturnRepository>();

            return new ReturnRegistrationService(deliveryRepository, returnRepository);
        }

        private IDeliveryRegistrationService CreateDeliveryRegistrationService()
        {
            var carCategoryProvider = GetSingletonInstance<IPriceCalculationStrategyProvider>();
            var deliveryRepository = GetSingletonInstance<IDeliveryRepository>();
            return new DeliveryRegistrationService(carCategoryProvider, deliveryRepository);
        }

        public void RegisterSingleton<T>(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            singletonInstances.Add(typeof(T), instance);
        }

        public T GetSingletonInstance<T>()
        {
            singletonInstances.TryGetValue(typeof(T), out var instance);

            if (instance == null)
            {
                throw new ContainerResolutionException($"Failed to resolve {typeof(T).FullName}");
            }

            return (T)instance;
        }
    }
}
