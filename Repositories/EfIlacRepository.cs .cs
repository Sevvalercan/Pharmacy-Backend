using Pharmacy_Backend.Core.Data.Ef;
using Pharmacy_Backend.Data;
using Pharmacy_Backend.Models;

namespace Pharmacy_Backend.Repositories
{
    public class EfIlacRepository:EfEntityRepository<Ilac, PharmacyBackendDbContext>,IIlacRepositories
    {

    }
}
