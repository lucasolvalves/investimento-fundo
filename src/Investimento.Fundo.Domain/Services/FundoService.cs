using Investimento.Fundo.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Fundo.Domain.Services
{
    public class FundoService : IFundoService
    {
        private readonly IB3FundoService _b3FundoService;

        public FundoService(IB3FundoService b3FundoService)
        {
            _b3FundoService = b3FundoService;
        }

        public async Task<List<Entities.Fundo>> GetAllByAccountIdAsync(long accountId)
        {
            return await _b3FundoService.GetAllByAccountIdAsync(accountId);
        }
    }
}
