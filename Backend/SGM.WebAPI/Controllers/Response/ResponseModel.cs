using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.WebAPI.Controllers.Response {
	public class ResponseModel {
		public int StatusCode { get; set; }
		public ResponseType Type { get; set; }
		public string Message { get; set; }
		public object Content { get; set; }

		public enum ResponseType {
			Success,
			Error,
			Attention
		}
	}
}
