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
    public class ModulosController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ModulosController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Modulos r)
        {
            if (r.IdModulo == 0 && r.IdClasmod == 0)
            {
                List<Modulos> claseList = await ctx.Modulos.ToListAsync();
                List<Modulos> listClase = new List<Modulos>();
                foreach (var clase in claseList)
                {
                    Modulos revistals = new Modulos
                    {
                        IdModulo = clase.IdModulo,
                        IdClasmod = clase.IdClasmod,
                        TituloModulo = clase.TituloModulo,
                        ProgresoModulo = clase.ProgresoModulo,
                        EstadoModulo = clase.EstadoModulo
                    };

                    listClase.Add(revistals);
                }

                reply.ok = true;
                reply.data = listClase;
            }
            else if (r.IdModulo == 0 && r.IdClasmod != 0)
            {
                var listModulo = await ctx.Modulos.Where(e => e.IdModulo == r.IdModulo).ToListAsync();
                List<Modulos> revistaList = new List<Modulos>();

                if (listModulo.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese modulo registrada registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var clase in listModulo)
                    {
                        Modulos revistals = new Modulos
                        {
                            IdModulo = clase.IdModulo,
                            IdClasmod = clase.IdClasmod,
                            TituloModulo = clase.TituloModulo,
                            ProgresoModulo = clase.ProgresoModulo,
                            EstadoModulo = clase.EstadoModulo
                        };

                        revistaList.Add(revistals);
                    }

                    reply.ok = true;
                    reply.data = revistaList;

                    return Ok(reply);
                }
            }
            else if (r.IdModulo != 0 && r.IdClasmod != 0)
            {
                var clase = await ctx.Modulos.FirstOrDefaultAsync(e => e.IdModulo == r.IdModulo && e.IdClasmod == r.IdClasmod);

                if (clase == null)
                {
                    reply.ok = false;
                    reply.data = "Modulo no encontrada";

                }
                else
                {
                    Modulos revistals = new Modulos
                    {
                        IdModulo = clase.IdModulo,
                        IdClasmod = clase.IdClasmod,
                        TituloModulo = clase.TituloModulo,
                        ProgresoModulo = clase.ProgresoModulo,
                        EstadoModulo = clase.EstadoModulo
                    };

                    reply.ok = true;
                    reply.data = revistals;
                }
            }
            else if (r.IdModulo != 0 && r.IdClasmod == 0)
            {
                var listrevista = await ctx.Modulos.Where(e => e.IdModulo == r.IdModulo).ToListAsync();
                List<Modulos> seccList = new List<Modulos>();

                if (listrevista.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese modulo registrada";

                    return Ok(reply);
                }
                else
                {
                    foreach (var clase in listrevista)
                    {
                        Modulos seccionls = new Modulos
                        {
                            IdModulo = clase.IdModulo,
                            IdClasmod = clase.IdClasmod,
                            TituloModulo = clase.TituloModulo,
                            ProgresoModulo = clase.ProgresoModulo,
                            EstadoModulo = clase.EstadoModulo
                        };

                        seccList.Add(seccionls);
                    }

                    reply.ok = true;
                    reply.data = seccList;
                    return Ok(reply);
                }
            }
            else if (r.IdModulo == 0 && r.IdClasmod != 0)
            {
                var listrevista = await ctx.Modulos.Where(e => e.IdClasmod == r.IdClasmod).ToListAsync();
                List<Modulos> seccList = new List<Modulos>();

                if (listrevista.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa clase registrada";

                    return Ok(reply);
                }
                else
                {
                    foreach (var clase in listrevista)
                    {
                        Modulos seccionls = new Modulos
                        {
                            IdModulo = clase.IdModulo,
                            IdClasmod = clase.IdClasmod,
                            TituloModulo = clase.TituloModulo,
                            ProgresoModulo = clase.ProgresoModulo,
                            EstadoModulo = clase.EstadoModulo
                        };

                        seccList.Add(seccionls);
                    }

                    reply.ok = true;
                    reply.data = seccList;
                    return Ok(reply);
                }
            }
            return Ok(reply);
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(Modulos t)
        {
            try
            {
                var u = await ctx.Modulos.FirstOrDefaultAsync(e => e.TituloModulo == t.TituloModulo);
                //Insertar
                if (t.IdModulo == 0 && u != null)
                {

                    reply.ok = false;
                    reply.data = "Nombre de modulo en uso";

                }

                else if (t.IdModulo == 0 && u == null)
                {
                    ctx.Modulos.Add(t);

                    reply.ok = true;
                    reply.data = t;
                }
                //Actualizar
                else if (t.IdModulo != 0 && u == null)
                {
                    var userName = await ctx.Modulos.FirstOrDefaultAsync(e => e.IdModulo == t.IdModulo);

                    userName.IdModulo = t.IdModulo;
                    userName.IdClasmod = t.IdClasmod;
                    userName.TituloModulo = t.TituloModulo;
                    userName.ProgresoModulo = t.ProgresoModulo;
                    userName.EstadoModulo = t.EstadoModulo;



                    ctx.Entry(userName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = t;
                }
                else if (t.IdModulo != 0 && u != null && t.IdModulo != u.IdModulo)
                {
                    reply.ok = false;
                    reply.data = "Nombre de modulo en uso";

                }
                else if (t.IdModulo != 0 && u != null && t.IdModulo == u.IdModulo)
                {

                    var userName = await ctx.Modulos.FirstAsync(e => e.IdModulo == u.IdModulo);

                    userName.IdModulo = t.IdModulo;
                    userName.IdClasmod = t.IdClasmod;
                    userName.TituloModulo = t.TituloModulo;
                    userName.ProgresoModulo = t.ProgresoModulo;
                    userName.EstadoModulo = t.EstadoModulo;

                    ctx.Entry(userName).CurrentValues.SetValues(userName);

                    reply.ok = true;
                    reply.data = userName;

                }
                await ctx.SaveChangesAsync();
                return Ok(reply);
            }
            catch
            {
                reply.ok = false;
                reply.data = "Error";
                return BadRequest(reply);
            }
        }

    }
}
