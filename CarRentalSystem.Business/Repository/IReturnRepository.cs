using CarRentalSystem.Business.Model;

namespace CarRentalSystem.Business.Repository;

public interface IReturnRepository
{
    CarReturn? Get(long bookingNumber);
    CarReturn Create(CarReturn newCarReturn);
    void Update(CarReturn updatedCarReturn);
    void Delete(long bookingNumber);
}