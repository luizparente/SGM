using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGM.WebAPI.Controllers.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.WebAPI.Controllers {
	[Route("[controller]")]
	[ApiController]
	public class TestController : ControllerBase {
		[HttpGet]
		public async Task<IActionResult> OnGetAsync() {
			return new JsonResult(new ResponseModel() {
				StatusCode = 200,
				Type = ResponseModel.ResponseType.Success,
				Message = "System is running."
			});
		}
	}
}
