using Pharmacy_Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Backend.Core.Data
{
    public interface IQueryableRepository<T> where T : class, IEntity, new()
    {
    }
}
