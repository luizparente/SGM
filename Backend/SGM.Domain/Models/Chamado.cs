using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public abstract class Chamado {
		public string GUID { get; set; }
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		public DateTime AbertoEm { get; set; }
		public DateTime? FechadoEm { get; set; }
		public string Observacoes { get; set; }
		public Status Status { get; set; }
		public Usuario AbertoPor { get; set; }
		public Resolvedor Responsavel { get; set; }

		public override string ToString() {
			return $"GUID: {this.GUID}";
		}
	}

	public enum Status { 
		Aberto,
		EmAndamento,
		Concluido
	}
}
