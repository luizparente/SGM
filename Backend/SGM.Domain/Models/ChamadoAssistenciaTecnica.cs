using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class ChamadoAssistenciaTecnica : Chamado {
		public Tipo Tipo { get; set; }
	}

	public enum Tipo {
		Defeito,
		Dados,
		Funcionalidade,
		Outro
	}
}
