using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class TecnicoDeCampo : Resolvedor {
		public string CREA { get; set; }
		public IEnumerable<Especialidade> Especialidades { get; set; }
		public EquipeDeCampo Equipe { get; set; }
	}
}
