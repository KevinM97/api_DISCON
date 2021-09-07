using api_DISCON.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api_DISCON.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly disconCTX ctx;
        private readonly IConfiguration config;

        Respuesta respuesta = new Respuesta();

        public LoginController(disconCTX _ctx, IConfiguration _config)
        {
            ctx = _ctx;
            config = _config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(Models.LoginVM Login)
        {

            Credenciales usuario = await ctx.Credenciales.Where(x => x.UsernameCreden.Trim() == Login.nombreUsuario.Trim() && x.PasswordCreden.Trim() == Login.passUsuario.Trim()).FirstOrDefaultAsync();

            try
            {

                if (usuario == null)
                {
                    respuesta.ok = false;
                    respuesta.data = "Credenciales incorrectos";

                    return Ok(respuesta);
                }
                else
                {
                    Usuarios user = await ctx.Usuarios.Where(x => x.IdCreden == usuario.IdCreden).FirstOrDefaultAsync();
                    var secretKey = config.GetValue<string>("SecretKey");
                    var key = Encoding.ASCII.GetBytes(secretKey);

                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Login.nombreUsuario));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddHours(8760),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                    string bearer_token = tokenHandler.WriteToken(createdToken);

                    infoUser infoUser = new infoUser();


                    infoUser.username = Login.nombreUsuario;
                    infoUser.tokenUsuario = bearer_token;
                    infoUser.idCreden = usuario.IdCreden;
                    infoUser.IdUsuario = user.IdUsuario;
                    infoUser.NombreUsuario = user.NombreUsuario;
                    infoUser.EmailUsuario = user.EmailUsuario;
                    infoUser.EstadoUsuario = user.EstadoUsuario;


                    respuesta.ok = true;
                    respuesta.data = infoUser;

                    return Ok(respuesta);
                }
            }
            catch
            {
                respuesta.ok = false;
                respuesta.data = "Ocurrio un error.";
                return BadRequest(respuesta);
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            if (r == null)
            {
                respuesta.ok = false;
            }
            else
            {
                respuesta.ok = true;
            }

            respuesta.data = r;
            return Ok(respuesta);
        }

    }
}
