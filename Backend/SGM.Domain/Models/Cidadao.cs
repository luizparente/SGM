using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class Cidadao : Usuario {
		public Endereco Endereco { get; set; }
		public IEnumerable<Chamado> Chamados { get; set; }
	}
}
