using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class Endereco {
		public string GUID { get; set; }
		public string Linha1 { get; set; }
		public string Linha2 { get; set; }
		public string Linha3 { get; set; }
		public string Cidade { get; set; } = "Luizlandia";
		public string Estado { get; set; } = "SP";
		public string CEP { get; set; }
		public string Pais { get; set; } = "Brasil";
		public Finalidade Finalidade { get; set; }

		public override string ToString() {
			return $"GUID: {this.GUID}";
		}
	}

	public enum Finalidade {
		Residencial,
		Comercial
	}
}
