using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class Permissao {
		public string GUID { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }

		public override string ToString() {
			return $"GUID: {this.GUID} \n" +
				   $"Nome: {this.Nome} \n" +
				   $"Descricao: {this.Descricao}";
		}
	}
}
