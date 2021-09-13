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
    public class UsuarioCursosController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public UsuarioCursosController(disconCTX _ctx)
        {
            ctx = _ctx; 
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(UsuarioCursos ts)
        {
            if (ts.IdUsuario == 0 && ts.IdCurso == 0)
            {
                List<UsuarioCursos> tiendasList = await ctx.UsuarioCursos.ToListAsync();
                List<UsuarioCursos> listTiendas = new List<UsuarioCursos>();
                foreach (var curs in tiendasList)
                {
                    UsuarioCursos cursols = new UsuarioCursos
                    {
                        IdUsucu = curs.IdUsucu,
                        IdUsuario = curs.IdUsuario,
                        IdCurso = curs.IdCurso
                    };

                    listTiendas.Add(cursols);
                }

                reply.ok = true;
                reply.data = listTiendas;
            }
            else if (ts.IdUsuario == 0 && ts.IdCurso != 0)
            {
                var listTiendas = await ctx.UsuarioCursos.Where(e => e.IdCurso == ts.IdCurso).ToListAsync();
                List<UsuarioCursos> tiendaList = new List<UsuarioCursos>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese curso registrado para ese usuario";

                    return Ok(reply);
                }
                else
                {
                    foreach (var curs in listTiendas)
                    {
                        UsuarioCursos tiendasls = new UsuarioCursos
                        {
                            IdUsucu = curs.IdUsucu,
                            IdUsuario = curs.IdUsuario,
                            IdCurso = curs.IdCurso
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            else if (ts.IdUsuario != 0 && ts.IdCurso != 0)
            {
                var curs = await ctx.UsuarioCursos.FirstOrDefaultAsync(e => e.IdUsuario == ts.IdUsuario && e.IdCurso == ts.IdCurso);

                if (curs == null)
                {
                    reply.ok = false;
                    reply.data = "Usuario no encontrado";

                }
                else
                {
                    UsuarioCursos tiendals = new UsuarioCursos
                    {
                        IdUsucu = curs.IdUsucu,
                        IdUsuario = curs.IdUsuario,
                        IdCurso = curs.IdCurso
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            else if (ts.IdUsuario != 0 && ts.IdCurso == 0)
            {
                var listTiendas = await ctx.UsuarioCursos.Where(e => e.IdUsuario == ts.IdUsuario).ToListAsync();
                List<UsuarioCursos> tiendaList = new List<UsuarioCursos>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa clase registrado";

                    return Ok(reply);
                }
                else
                {
                    foreach (var curs in listTiendas)
                    {
                        UsuarioCursos tiendasls = new UsuarioCursos
                        {
                            IdUsucu = curs.IdUsucu,
                            IdUsuario = curs.IdUsuario,
                            IdCurso = curs.IdCurso
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            return Ok(reply);
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(UsuarioCursos t)
        {
            if (t.IdUsucu == 0)
            {
                ctx.UsuarioCursos.Add(t);
                reply.ok = true;
                reply.data = t;
            }
            else
            {
                var finName = await ctx.UsuarioCursos.FirstOrDefaultAsync(e => e.IdUsucu == t.IdUsucu);


                finName.IdUsucu = t.IdUsucu;
                finName.IdUsuario = t.IdUsuario;
                finName.IdCurso = t.IdCurso;



                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = t;
            }
            await ctx.SaveChangesAsync();
            return Created("Curso", reply);
        }

    }
}
