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
    [Authorize]
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
            if (c.IdCurso == 0 && c.IdUsuario == 0)
            {
                List<Curso> cursoList = await ctx.Curso.ToListAsync();
                List<Curso> listCurso = new List<Curso>();

                foreach (var curso in cursoList)
                {
                    Curso cursols = new Curso
                    {
                        IdCurso = curso.IdCurso,
                        IdUsuario = curso.IdUsuario,
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
            else if (c.IdCurso == 0 && c.IdUsuario != 0)
            {
                var listCurso = await ctx.Curso.Where(e => e.IdUsuario == c.IdUsuario).ToListAsync();
                List<Curso> CursoList = new List<Curso>();

                if (listCurso.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No hay curso registrado para ese usuario";

                    return Ok(reply);
                }
                else
                {
                    foreach (var curso in listCurso)
                    {
                        Curso cursols = new Curso
                        {
                            IdCurso = curso.IdCurso,
                            IdUsuario = curso.IdUsuario,
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
            else if (c.IdCurso!= 0 && c.IdUsuario != 0)
            {
                var curso = await ctx.Curso.FirstOrDefaultAsync(e => e.IdCurso == c.IdCurso && e.IdUsuario == c.IdUsuario);
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
                        IdUsuario = curso.IdUsuario,
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

    }
}
