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
    public class PreguntasClasController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public PreguntasClasController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(PreguntasClas pc)
        {
            if (pc.IdPregunta == 0)
            {
                List<PreguntasClas> listaResp = await ctx.PreguntasClas.ToListAsync();
                List<PreguntasClas> respList = new List<PreguntasClas>();

                foreach (var preguntas in listaResp)
                {

                    PreguntasClas resLis = new PreguntasClas
                    {
                        IdPregunta = preguntas.IdPregunta,
                        TituloPregunta = preguntas.TituloPregunta,
                        ValorPregunta = preguntas.ValorPregunta,
                        EstadoPregunta = preguntas.EstadoPregunta

                    };


                    respList.Add(resLis);
                }

                reply.ok = true;
                reply.data = respList;

                return Ok(reply);
            }
            else
            {
                var pregunta = await ctx.PreguntasClas.FirstOrDefaultAsync(e => e.IdPregunta == pc.IdPregunta);

                if (pregunta == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    PreguntasClas lsCreden = new PreguntasClas();
                    lsCreden.IdPregunta = pregunta.IdPregunta;
                    lsCreden.TituloPregunta = pregunta.TituloPregunta;
                    lsCreden.ValorPregunta = pregunta.ValorPregunta;
                    lsCreden.EstadoPregunta = pregunta.EstadoPregunta;


                    reply.ok = true;
                    reply.data = pregunta;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(PreguntasClas pe)
        {

            if (pe.IdPregunta == 0)
            {
                ctx.PreguntasClas.Add(pe);

                reply.ok = true;
                reply.data = pe;
            }
            else if (pe.IdPregunta != 0)
            {
                var finName = await ctx.PreguntasClas.FirstOrDefaultAsync(e => e.IdPregunta == pe.IdPregunta);


                finName.IdPregunta = pe.IdPregunta;
                finName.TituloPregunta = pe.TituloPregunta;
                finName.ValorPregunta = pe.ValorPregunta;
                finName.EstadoPregunta = pe.EstadoPregunta;



                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = pe;
            }
            await ctx.SaveChangesAsync();
            return Created("Pregunta", reply);



        }
    }
}
