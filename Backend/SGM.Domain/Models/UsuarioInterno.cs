using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class UsuarioInterno : Usuario {
		public string Matricula { get; set; }
		public bool Ativo { get; set; }
	}
}
