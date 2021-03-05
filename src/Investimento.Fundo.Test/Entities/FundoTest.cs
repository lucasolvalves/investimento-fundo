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
        public void ShouldCalculateIROfFundoWhenDataIsValid()
        {
            var fundo = new Domain.Entities.Fundo(1000, 1159, DateTime.Parse("2022-10-01T00:00:00"), DateTime.Parse("2017-10-01T00:00:00"), 0, "ALASKA", 53.49, 1);
            Assert.IsTrue(fundo.IR == 23.849999999999998);
        }

        [TestMethod]
        public void ShouldNotCalculateIROfFundoWhenDataIsInvalid()
        {
            var fundo = new Domain.Entities.Fundo(0, 0, DateTime.Parse("2022-10-01T00:00:00"), DateTime.Parse("2017-10-01T00:00:00"), 0, "ALASKA", 53.49, 1);
            Assert.IsTrue(fundo.IR == 0);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThan3MonthsExpirationDateofFundoWhenDataIsValid()
        {
            var fundo = new Domain.Entities.Fundo(1000, 1159, DateTime.Now.AddMonths(3), DateTime.Now.AddMonths(-3), 0, "ALASKA", 53.49, 1);
            Assert.IsTrue(fundo.ValorResgate == 1089.46);
        }

        [TestMethod]
        public void ShouldCalculateResgateMoreThanHalfExpirationDateofFundoWhenDataIsValid()
        {
            var fundo = new Domain.Entities.Fundo(1000, 1159, DateTime.Now.AddMonths(5), DateTime.Now.AddMonths(-7), 0, "ALASKA", 53.49, 1);
            Assert.IsTrue(fundo.ValorResgate == 985.15);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThanHalfExpirationDateofFundoWhenDataIsValid()
        {
            var fundo = new Domain.Entities.Fundo(1000, 1159, DateTime.Now.AddMonths(12), DateTime.Now.AddMonths(-7), 0, "ALASKA", 53.49, 1);
            Assert.IsTrue(fundo.ValorResgate == 811.3);
        }
    }
}
