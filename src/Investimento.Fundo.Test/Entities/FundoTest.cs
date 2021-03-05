using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Fundo.Test.Entities
{
    [TestClass]
    public class FundoTest
    {
        [TestMethod]
        public void ShouldCalculateIROfRendaFixaoWhenDataIsValid()
        {
            var rendaFixa = new Domain.Entities.RendaFixa(2000.0, 2097.85, 2.0, DateTime.Parse("2021-03-09T00:00:00"), 0.0, 0.0, 0.0, "97% do CDI", "LCI", "BANCO MAXIMA", true, DateTime.Parse("2019-03-14T00:00:00"), 1048.927450, false);

            Assert.IsTrue(rendaFixa.IR == 4.8924999999999956);
        }
    }
}
