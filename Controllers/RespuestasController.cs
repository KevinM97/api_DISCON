using api_DISCON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestasController : ControllerBase
    {
        private readonly disconCTX ctx;

        Respuesta reply = new Respuesta();

        public RespuestasController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Respuestas r)
        {
            if (r.IdRespuesta == 0)
            {
                List<Respuestas> listaResp = await ctx.Respuestas.ToListAsync();
                List<Respuestas> respList = new List<Respuestas>();

                foreach (var respuesta in listaResp)
                {

                    Respuestas resLis = new Respuestas
                    {
                        IdRespuesta = respuesta.IdRespuesta,
                        TituloRespuesta = respuesta.TituloRespuesta,
                        ValorRespuesta = respuesta.ValorRespuesta,
                        EstadoRespuesta = respuesta.EstadoRespuesta
                    };


                    respList.Add(resLis);
                }

                reply.ok = true;
                reply.data = respList;

                return Ok(reply);
            }
            else
            {
                var respuesta = await ctx.Respuestas.FirstOrDefaultAsync(e => e.IdRespuesta == r.IdRespuesta);

                if (respuesta == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Respuestas lsCreden = new Respuestas();
                    lsCreden.IdRespuesta = respuesta.IdRespuesta;
                    lsCreden.TituloRespuesta = respuesta.TituloRespuesta;
                    lsCreden.ValorRespuesta = respuesta.ValorRespuesta;
                    lsCreden.EstadoRespuesta = respuesta.EstadoRespuesta;


                    reply.ok = true;
                    reply.data = respuesta;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(Respuestas re)
        {

            if (re.IdRespuesta == 0)
            {
                ctx.Respuestas.Add(re);

                reply.ok = true;
                reply.data = re;
            }
            else if (re.IdRespuesta != 0)
            {
                var finName = await ctx.Respuestas.FirstOrDefaultAsync(e => e.IdRespuesta == re.IdRespuesta);


                finName.IdRespuesta = re.IdRespuesta;
                finName.TituloRespuesta = re.TituloRespuesta;
                finName.ValorRespuesta = re.ValorRespuesta;
                finName.EstadoRespuesta = re.EstadoRespuesta;



                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = re;
            }
            await ctx.SaveChangesAsync();
            return Created("Respuesta", reply);



        }
    }
}
