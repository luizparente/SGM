using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class Propriedade {
		public string GUID { get; set; }
		public string Matricula { get; set; }
		public Endereco Endereco { get; set; }
		public decimal AreaTerreno { get; set; }
		public decimal AreaConstruida { get; set; }
		public string Observacoes { get; set; }

		public override string ToString() {
			return $"GUID: {this.GUID}";
		}
	}
}
