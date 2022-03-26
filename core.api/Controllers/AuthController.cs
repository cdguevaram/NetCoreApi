using core.Api.Model;
using core.Business.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            this._config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {           
            try
            {
                ExternalData externaData = new ExternalData(_config["EsternalApi:url"]);
                var users = await externaData.GetAllUsers();
                if (users == null || users.Count == 0) throw new Exception("Imposible recupear usuarios");

                var user = users.Where(p => p.userName == model.UserName && p.password == model.Password).FirstOrDefault();
                if (user == null) throw new Exception("No existe usuario");
                    
                List<string> roles = new List<string>() { "admin" };
                var token=TokenHandler.GenerateJwt(model, _config["Jwt:Key"], _config["Jwt:Issuer"], roles);
                return Ok(token);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
