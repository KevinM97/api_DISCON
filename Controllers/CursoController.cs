using api_DISCON.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class CursoController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public CursoController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Curso c)
        {
            if (c.IdCurso == 0 && c.IdModulo == 0)
            {
                List<Curso> cursoList = await ctx.Curso.ToListAsync();
                List<Curso> listCurso = new List<Curso>();

                foreach (var curso in cursoList)
                {
                    Curso cursols = new Curso
                    {
                        IdCurso = curso.IdCurso,
                        IdModulo = curso.IdModulo,
                        NombreCurso = curso.NombreCurso,
                        DescripcionCurso = curso.DescripcionCurso,
                        ImagenCurso = curso.ImagenCurso
                    };

                    listCurso.Add(cursols);
                }

                reply.ok = true;
                reply.data = listCurso;


            }
            else if (c.IdCurso == 0 && c.IdModulo != 0)
            {
                var listCurso = await ctx.Curso.Where(e => e.IdModulo == c.IdModulo).ToListAsync();
                List<Curso> CursoList = new List<Curso>();

                if (listCurso.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No hay modulo registrado para ese usuario";

                    return Ok(reply);
                }
                else
                {
                    foreach (var curso in listCurso)
                    {
                        Curso cursols = new Curso
                        {
                            IdCurso = curso.IdCurso,
                            IdModulo = curso.IdModulo,
                            NombreCurso = curso.NombreCurso,
                            DescripcionCurso = curso.DescripcionCurso,
                            ImagenCurso = curso.ImagenCurso
                        };


                        CursoList.Add(cursols);
                    }
                    reply.ok = true;
                    reply.data = CursoList;

                    return Ok(reply);
                }

            }
            else if (c.IdCurso!= 0 && c.IdModulo != 0)
            {
                var curso = await ctx.Curso.FirstOrDefaultAsync(e => e.IdCurso == c.IdCurso && e.IdModulo == c.IdModulo);
                if (curso == null)
                {
                    reply.ok = false;
                    reply.data = "Curso no encontrado";
                }
                else
                {
                    Curso ventasls = new Curso
                    {
                        IdCurso = curso.IdCurso,
                        IdModulo = curso.IdModulo,
                        NombreCurso = curso.NombreCurso,
                        DescripcionCurso = curso.DescripcionCurso,
                        ImagenCurso = curso.ImagenCurso
                    };

                    reply.ok = true;
                    reply.data = ventasls;
                }
            }

            return Ok(reply);
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(Curso t)
        {
            if (t.IdCurso == 0)
            {
                var Modulo = await ctx.Curso.FirstOrDefaultAsync(e => e.IdModulo == t.IdModulo);

                if (Modulo == null)
                {
                    reply.ok = false;
                    reply.data = "No existe ese Modulo";

                    return Ok(reply);
                }
                else
                {
                    ctx.Curso.Add(t);

                    reply.ok = true;
                    reply.data = t;
                }
            }
            else if (t.IdCurso != 0)
            {
                var finName = await ctx.Curso.FirstOrDefaultAsync(e => e.IdCurso == t.IdCurso);


                finName.IdCurso = t.IdCurso;
                finName.IdModulo = t.IdModulo;
                finName.NombreCurso = t.NombreCurso;
                finName.DescripcionCurso = t.DescripcionCurso;
                finName.ImagenCurso = t.ImagenCurso;
                finName.EstadoCurso = t.EstadoCurso;



                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = t;
            }
            await ctx.SaveChangesAsync();
            return Created("Curso", reply);
        }

    }
}
