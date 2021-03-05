using Investimento.Fundo.Infrastructure.Services;
using Investimento.Fundo.Test.Builders;
using KissLog;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.Fundo.Test.Services
{
    [TestClass]
    public class B3FundoServiceTest
    {
        private Mock<IHttpClientFactory> _httpFactory;
        private Mock<ILogger> _logger;
        private Mock<IConfigurationSection> _configurationSection;
        private const string JsonSucess = "{\r\n\"fundos\": [{\r\n\t\t\t\"capitalInvestido\": 1000,\r\n\t\t\t\"ValorAtual\": 1159,\r\n\t\t\t\"dataResgate\": \"2022-10-01T00:00:00\",\r\n\t\t\t\"dataCompra\": \"2017-10-01T00:00:00\",\r\n\t\t\t\"iof\": 0,\r\n\t\t\t\"nome\": \"ALASKA\",\r\n\t\t\t\"totalTaxas\": 53.49,\r\n\t\t\t\"quantity\": 1\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"capitalInvestido\": 10000.0,\r\n\t\t\t\"ValorAtual\": 12300.52,\r\n\t\t\t\"dataResgate\": \"2022-11-15T00:00:00\",\r\n\t\t\t\"dataCompra\": \"2019-11-15T00:00:00\",\r\n\t\t\t\"iof\": 0,\r\n\t\t\t\"nome\": \"REAL\",\r\n\t\t\t\"totalTaxas\": 134.49,\r\n\t\t\t\"quantity\": 1\r\n\t\t}\r\n\t]\r\n}";

        [TestInitialize]
        public void Initialize()
        {
            _httpFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger>();
            _configurationSection = new Mock<IConfigurationSection>();
        }

        [TestMethod]
        public async Task ShouldReturnFundoWhenB3ServiceIsUp()
        {
            _configurationSection.Setup(x => x.GetSection(It.IsAny<string>())).Returns(CreateFakeConfigurationSection());

            var client = new HttpClient(HttpMessageHandlerBuilder.Create(HttpStatusCode.OK, JsonSucess, HttpMethod.Get, "http://test.com.br/unitest"))
            {
                BaseAddress = new Uri("http://test.com.br/")
            };

            _httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new B3FundoService(_httpFactory.Object, _logger.Object, _configurationSection.Object);
            var result = await service.GetAllByAccountIdAsync(1234567899);

            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public async Task ShouldNotReturnFundoWhenB3ServiceReturnUnsuccessfully()
        {
            _configurationSection.Setup(x => x.GetSection(It.IsAny<string>())).Returns(CreateFakeConfigurationSection());

            var client = new HttpClient(HttpMessageHandlerBuilder.Create(HttpStatusCode.InternalServerError, "", HttpMethod.Get, "http://test.com.br/unitest"))
            {
                BaseAddress = new Uri("http://test.com.br/")
            };

            _httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new B3FundoService(_httpFactory.Object, _logger.Object, _configurationSection.Object);
            var result = await service.GetAllByAccountIdAsync(1234567899);

            Assert.IsTrue(result.Count == 0);
        }

        private IConfigurationSection CreateFakeConfigurationSection()
        {
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x.Path).Returns("AppSettings:Mockyio:RequestUri");
            configurationSection.Setup(x => x.Key).Returns("RequestUri");
            configurationSection.Setup(x => x.Value).Returns("http://test.com.br/configurationSection");
            return configurationSection.Object;
        }
    }
}
