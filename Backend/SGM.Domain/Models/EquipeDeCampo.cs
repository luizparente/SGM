using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class EquipeDeCampo {
		public string GUID { get; set; }
		public GerenteDeCampo Gestor { get; set; }
		public IEnumerable<TecnicoDeCampo> Membros { get; set; }
	}
}
