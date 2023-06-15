using CarRentalSystem.Business.Exception;
using CarRentalSystem.Business.Model;
using CarRentalSystem.Business.Repository;
using CarRentalSystem.Business.Service;
using CarRentalSystem.Business.Utils;

namespace CarRentalSystem.Business.Infrastructure
{
    public class IocContainer
    {
        private readonly Dictionary<Type, object> singletonInstances = new();

        public IocContainer()
        {
            RegisterSingleton(new PriceByCategoryStrategyProvider());
            RegisterSingleton(new BookingNumberGenerator());
            RegisterSingleton<IDeliveryRepository>(CreateDeliveryRepository());
            RegisterSingleton<IReturnRepository>(new ReturnRepository());
            RegisterSingleton(CreateDeliveryRegistrationService());
            RegisterSingleton(CreateReturnRegistrationService());
            RegisterSingleton(CreateRentalPriceCalculationService());
        }

        private RentalSummaryGeneratorService CreateRentalPriceCalculationService()
        {
            var deliveryRepository = GetSingletonInstance<IDeliveryRepository>();
            var returnRepository = GetSingletonInstance<IReturnRepository>();
            var priceByCategoryStrategyProvider = GetSingletonInstance<PriceByCategoryStrategyProvider>();
            return new RentalSummaryGeneratorService(deliveryRepository, returnRepository, priceByCategoryStrategyProvider);
        }

        private ReturnRegistrationService CreateReturnRegistrationService()
        {
            var deliveryRepository = GetSingletonInstance<IDeliveryRepository>();
            var returnRepository = GetSingletonInstance<IReturnRepository>();

            return new ReturnRegistrationService(deliveryRepository, returnRepository);
        }

        private DeliveryRegistrationService CreateDeliveryRegistrationService()
        {
            var carCategoryProvider = GetSingletonInstance<PriceByCategoryStrategyProvider>();
            var deliveryRepository = GetSingletonInstance<IDeliveryRepository>();
            return new DeliveryRegistrationService(carCategoryProvider, deliveryRepository);
        }

        private DeliveryRepository CreateDeliveryRepository()
        {
            var bookingNumberGenerator = GetSingletonInstance<BookingNumberGenerator>();
            return new DeliveryRepository(bookingNumberGenerator);
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
