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
    public class ClasemoduloController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ClasemoduloController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Clasemodulo r)
        {
            if (r.IdClasmod == 0 && r.IdPregunta == 0)
            {
                List<Clasemodulo> claseList = await ctx.Clasemodulo.ToListAsync();
                List<Clasemodulo> listClase = new List<Clasemodulo>();
                foreach (var clase in claseList)
                {
                    Clasemodulo revistals = new Clasemodulo
                    {
                        IdClasmod = clase.IdClasmod,
                        IdPregunta = clase.IdPregunta,
                        NombreClasmod = clase.NombreClasmod,
                        VideoClasmod = clase.VideoClasmod,
                        ValorClasmod = clase.ValorClasmod,
                        EstadoClasmod = clase.EstadoClasmod
                    };

                    listClase.Add(revistals);
                }

                reply.ok = true;
                reply.data = listClase;
            }
            else if (r.IdClasmod == 0 && r.IdPregunta != 0)
            {
                var listRevista = await ctx.Clasemodulo.Where(e => e.IdClasmod == r.IdClasmod).ToListAsync();
                List<Clasemodulo> revistaList = new List<Clasemodulo>();

                if (listRevista.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa clase registrada registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var clase in listRevista)
                    {
                        Clasemodulo revistals = new Clasemodulo
                        {
                            IdClasmod = clase.IdClasmod,
                            IdPregunta = clase.IdPregunta,
                            NombreClasmod = clase.NombreClasmod,
                            VideoClasmod = clase.VideoClasmod,
                            ValorClasmod = clase.ValorClasmod,
                            EstadoClasmod = clase.EstadoClasmod
                        };

                        revistaList.Add(revistals);
                    }

                    reply.ok = true;
                    reply.data = revistaList;

                    return Ok(reply);
                }
            }
            else if (r.IdClasmod != 0 && r.IdPregunta != 0)
            {
                var clase = await ctx.Clasemodulo.FirstOrDefaultAsync(e => e.IdClasmod == r.IdClasmod && e.IdPregunta == r.IdPregunta);

                if (clase == null)
                {
                    reply.ok = false;
                    reply.data = "Clase no encontrada";

                }
                else
                {
                    Clasemodulo revistals = new Clasemodulo
                    {
                        IdClasmod = clase.IdClasmod,
                        IdPregunta = clase.IdPregunta,
                        NombreClasmod = clase.NombreClasmod,
                        VideoClasmod = clase.VideoClasmod,
                        ValorClasmod = clase.ValorClasmod,
                        EstadoClasmod = clase.EstadoClasmod
                    };

                    reply.ok = true;
                    reply.data = revistals;
                }
            }
            else if (r.IdClasmod != 0 && r.IdPregunta == 0)
            {
                var listrevista = await ctx.Clasemodulo.Where(e => e.IdClasmod == r.IdClasmod).ToListAsync();
                List<Clasemodulo> seccList = new List<Clasemodulo>();

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
                        Clasemodulo seccionls = new Clasemodulo
                        {
                            IdClasmod = clase.IdClasmod,
                            IdPregunta = clase.IdPregunta,
                            NombreClasmod = clase.NombreClasmod,
                            VideoClasmod = clase.VideoClasmod,
                            ValorClasmod = clase.ValorClasmod,
                            EstadoClasmod = clase.EstadoClasmod
                        };

                        seccList.Add(seccionls);
                    }

                    reply.ok = true;
                    reply.data = seccList;
                    return Ok(reply);
                }
            }
            else if (r.IdClasmod == 0 && r.IdPregunta != 0)
            {
                var listrevista = await ctx.Clasemodulo.Where(e => e.IdPregunta == r.IdPregunta).ToListAsync();
                List<Clasemodulo> seccList = new List<Clasemodulo>();

                if (listrevista.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa pregunta registrada";

                    return Ok(reply);
                }
                else
                {
                    foreach (var clase in listrevista)
                    {
                        Clasemodulo seccionls = new Clasemodulo
                        {
                            IdClasmod = clase.IdClasmod,
                            IdPregunta = clase.IdPregunta,
                            NombreClasmod = clase.NombreClasmod,
                            VideoClasmod = clase.VideoClasmod,
                            ValorClasmod = clase.ValorClasmod,
                            EstadoClasmod = clase.EstadoClasmod
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
        public async Task<IActionResult> Post(Clasemodulo re)
        {
            try
            {
                var u = await ctx.Clasemodulo.FirstOrDefaultAsync(e => e.NombreClasmod == re.NombreClasmod);
                //Insertar
                if (re.IdClasmod == 0 && u != null)
                {

                    reply.ok = false;
                    reply.data = "Nombre de clase en uso";

                }

                else if (re.IdClasmod == 0 && u == null) 
                {
                    ctx.Clasemodulo.Add(re);

                    reply.ok = true;
                    reply.data = re;
                }
                //Actualizar
                else if (re.IdClasmod != 0 && u == null) 
                {
                    var revistaName = await ctx.Clasemodulo.FirstOrDefaultAsync(e => e.IdClasmod == re.IdClasmod);

                    revistaName.IdClasmod = re.IdClasmod;
                    revistaName.IdPregunta = re.IdPregunta;
                    revistaName.NombreClasmod = re.NombreClasmod;
                    revistaName.VideoClasmod = re.VideoClasmod;
                    revistaName.ValorClasmod = re.ValorClasmod;
                    revistaName.EstadoClasmod = re.EstadoClasmod;


                    ctx.Entry(revistaName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = re;
                }
                else if (re.IdClasmod != 0 && u != null && re.IdClasmod != u.IdClasmod)
                {
                    reply.ok = false;
                    reply.data = "Nombre de la clase en uso";

                }
                else if (re.IdClasmod != 0 && u != null && re.IdClasmod == u.IdClasmod) 
                {

                    var revistaName = await ctx.Clasemodulo.FirstAsync(e => e.IdClasmod == u.IdClasmod);

                    revistaName.IdClasmod = re.IdClasmod;
                    revistaName.IdPregunta = re.IdPregunta;
                    revistaName.NombreClasmod = re.NombreClasmod;
                    revistaName.VideoClasmod = re.VideoClasmod;
                    revistaName.ValorClasmod = re.ValorClasmod;
                    revistaName.EstadoClasmod = re.EstadoClasmod;

                    ctx.Entry(revistaName).CurrentValues.SetValues(revistaName);

                    reply.ok = true;
                    reply.data = revistaName;

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
