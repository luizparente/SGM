using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public abstract class Usuario {
		public string GUID { get; set; }
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public DateTime DataDeNascimento { get; set; }
		public string CPF { get; set; }
		public IEnumerable<Permissao> Permissoes { get; set; }

		public override string ToString() {
			return $"GUID: {this.GUID} \n" +
				   $"Nome: {this.Nome} {this.Sobrenome}\n" +
				   $"Data de nascimento: {this.DataDeNascimento:d}";
		}
	}
}
