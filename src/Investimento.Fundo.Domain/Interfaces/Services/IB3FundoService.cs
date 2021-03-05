using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Fundo.Domain.Interfaces.Services
{
    public interface IB3FundoService
    {
        Task<List<Entities.Fundo>> GetAllByAccountIdAsync(long accountId);
    }
}
