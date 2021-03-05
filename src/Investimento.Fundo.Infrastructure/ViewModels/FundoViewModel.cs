using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.Fundo.Infrastructure.ViewModels
{
	public class FundoViewModel
	{
		public double CapitalInvestido { get; set; }
		public double ValorAtual { get; set; }
		public DateTime DataResgate { get; set; }
		public DateTime DataCompra { get; set; }
		public double Iof { get; set; }
		public string Nome { get; set; }
		public double TotalTaxas { get; set; }
		public double Quantity { get; set; }
	}
}
