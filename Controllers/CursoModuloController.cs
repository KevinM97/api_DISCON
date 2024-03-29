﻿using api_DISCON.Models;
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
    public class CursoModuloController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public CursoModuloController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(CursoModulo ts)
        {
            if (ts.IdCurso == 0 && ts.IdModulo == 0)
            {
                List<CursoModulo> tiendasList = await ctx.CursoModulo.ToListAsync();
                List<CursoModulo> listTiendas = new List<CursoModulo>();
                foreach (var curs in tiendasList)
                {
                    CursoModulo cursols = new CursoModulo
                    {
                        IdCursomod = curs.IdCursomod,
                        IdCurso = curs.IdCurso,
                        IdModulo = curs.IdModulo
                    };

                    listTiendas.Add(cursols);
                }

                reply.ok = true;
                reply.data = listTiendas;
            }
            else if (ts.IdCurso == 0 && ts.IdModulo != 0)
            {
                var listTiendas = await ctx.CursoModulo.Where(e => e.IdModulo == ts.IdModulo).ToListAsync();
                List<CursoModulo> tiendaList = new List<CursoModulo>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese modulo registrado";

                    return Ok(reply);
                }
                else
                {
                    foreach (var curs in listTiendas)
                    {
                        CursoModulo tiendasls = new CursoModulo
                        {
                            IdCursomod = curs.IdCursomod,
                            IdCurso = curs.IdCurso,
                            IdModulo = curs.IdModulo
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            else if (ts.IdCurso != 0 && ts.IdModulo != 0)
            {
                var curs = await ctx.CursoModulo.FirstOrDefaultAsync(e => e.IdCurso == ts.IdCurso && e.IdModulo == ts.IdModulo);

                if (curs == null)
                {
                    reply.ok = false;
                    reply.data = "Curso no encontrado";

                }
                else
                {
                    CursoModulo tiendals = new CursoModulo
                    {
                        IdCursomod = curs.IdCursomod,
                        IdCurso = curs.IdCurso,
                        IdModulo = curs.IdModulo
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            else if (ts.IdCurso != 0 && ts.IdModulo == 0)
            {
                var listTiendas = await ctx.CursoModulo.Where(e => e.IdCurso == ts.IdCurso).ToListAsync();
                List<CursoModulo> tiendaList = new List<CursoModulo>();

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
                        CursoModulo tiendasls = new CursoModulo
                        {
                            IdCursomod = curs.IdCursomod,
                            IdCurso = curs.IdCurso,
                            IdModulo = curs.IdModulo
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
        public async Task<IActionResult> Post(CursoModulo t)
        {
            if (t.IdCursomod== 0)
            {
                ctx.CursoModulo.Add(t);
                reply.ok = true;
                reply.data = t;
            }
            else
            {
                var finName = await ctx.CursoModulo.FirstOrDefaultAsync(e => e.IdCursomod == t.IdCursomod);


                finName.IdCursomod = t.IdCursomod;
                finName.IdModulo = t.IdModulo;
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
