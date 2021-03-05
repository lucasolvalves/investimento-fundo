using Investimento.Fundo.Domain.Extensions;
using Investimento.Fundo.Domain.Interfaces.Services;
using Investimento.Fundo.Infrastructure.ViewModels;
using KissLog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.Fundo.Infrastructure.Services
{
    public class B3FundoService : IB3FundoService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public B3FundoService(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Domain.Entities.Fundo>> GetAllByAccountIdAsync(long accountId)
        {
            try
            {
                var listFundos = new List<Domain.Entities.Fundo>();
                var jsonString = await ConsumeEndpoint(accountId);
                var items = jsonString.JsonGetByName("fundos");

                if (string.IsNullOrEmpty(items))
                    return listFundos;

                var fundosViewModel = JsonConvert.DeserializeObject<List<FundoViewModel>>(items);
                fundosViewModel?.ForEach(x => listFundos.Add(new Domain.Entities.Fundo(x.CapitalInvestido, x.ValorAtual, x.DataResgate, x.DataCompra, x.Iof, x.Nome, x.TotalTaxas, x.Quantity)));

                return listFundos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> ConsumeEndpoint(long accountId)
        {
            try
            {
                using (HttpClient httpclient = _httpFactory.CreateClient("mockinvestimento"))
                using (HttpResponseMessage httpResponse = await httpclient.GetAsync(_configuration.GetSection("AppSettings:Mockyio:RequestUri").Value))
                {
                    var body = await httpResponse.Content?.ReadAsStringAsync();

                    _logger.Trace("RequestUrl: " + httpResponse.RequestMessage.RequestUri?.ToString() +
                                "\nMethod: " + httpResponse.RequestMessage.Method?.ToString() +
                                "\nResponseStatusCode: " + httpResponse?.StatusCode +
                                "\nResponseBody: " + body, "ConsomeEndpoint", 20);

                    return !string.IsNullOrWhiteSpace(body) ? body : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
