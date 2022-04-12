using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class GerenteDeCampo : Resolvedor {
		public string CREA { get; set; }
		public IEnumerable<EquipeDeCampo> Equipes { get; set; }
	}
}
