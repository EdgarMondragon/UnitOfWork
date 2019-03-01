using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IResponderRepository
    {
        Task<IEnumerable<Responder>> GetRespondersBySubscriberId(long id);
    }
}
