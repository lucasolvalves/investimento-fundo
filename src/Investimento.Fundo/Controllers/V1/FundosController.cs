using Investimento.Fundo.Domain.Interfaces.Services;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.Fundo.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fundos")]
    public class FundosController : MainController
    {
        private readonly IFundoService _fundoService;

        public FundosController(ILogger logger, IFundoService fundoService) : base(logger)
        {
            _fundoService = fundoService;
        }

        [HttpGet("{accountId:long}")]
        public async Task<IActionResult> GetInvestments(long accountId)
        {
            var Investments = await _fundoService.GetAllByAccountIdAsync(accountId);

            if (Investments?.Count == 0)
                return NotFound();

            return Ok(new { data = Investments });
        }
    }
}
