using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class ChamadoAssistenciaUrbana : Chamado {
		public Categoria Categoria { get; set; }
		public DateTime? AgendadoPara { get; set; }
		public Endereco Endereco { get; set; }
	}

	public enum Categoria {
		Iluminacao,
		Saneamento,
		Transito,
		Seguranca,
		Saude,
		Outro
	}
}
