using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Repository;

/// <summary>
/// Provides contracts for doing CRUD operations for car return registration into a database.
/// </summary>
public interface IReturnRepository
{
    CarReturn? Get(long bookingNumber);
    CarReturn Create(CarReturn newCarReturn);
    void Update(CarReturn updatedCarReturn);
    void Delete(long bookingNumber);
}