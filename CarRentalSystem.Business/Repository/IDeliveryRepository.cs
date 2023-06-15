using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Repository;

public interface IDeliveryRepository
{
    CarDelivery? Get(long bookingNumber);
    CarDelivery Create(CarDelivery carRental);
    void Update(CarDelivery carRental);
    void Delete(long bookingNumber);
}