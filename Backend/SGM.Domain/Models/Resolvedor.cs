﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Domain.Models {
	public class Resolvedor : UsuarioInterno {
		public IEnumerable<Chamado> Chamados { get; set; }
	}
}