using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Repository;

/// <summary>
/// Provides contracts for doing CRUD operations for car delivery registration into a database.
/// </summary>
public interface IDeliveryRepository
{
    CarDelivery? Get(long bookingNumber);
    CarDelivery Create(CarDelivery carRental);
    void Update(CarDelivery carRental);
    void Delete(long bookingNumber);
}