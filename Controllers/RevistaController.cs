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
    public class RevistaController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public RevistaController(disconCTX _ctx)
        {
            ctx = _ctx;
        }
        
        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Revista r)
        {
            if (r.IdRevista == 0 && r.IdSeccion == 0)
            {
                List<Revista> revistaList = await ctx.Revista.ToListAsync();
                List<Revista> listRevista = new List<Revista>();
                foreach (var revista in revistaList)
                {
                    Revista revistals = new Revista
                    {
                        IdRevista = revista.IdRevista,
                        IdSeccion = revista.IdSeccion,
                        NombreRevista = revista.NombreRevista,
                        EstadoRevista = revista.EstadoRevista,
                    };

                    listRevista.Add(revistals);
                }

                reply.ok = true;
                reply.data = listRevista;
            }
            else if (r.IdRevista == 0 && r.IdSeccion != 0)
            {
                var listRevista = await ctx.Revista.Where(e => e.IdRevista == r.IdRevista).ToListAsync();
                List<Revista> revistaList = new List<Revista>();

                if (listRevista.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa revista registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var revista in listRevista)
                    {
                        Revista revistals = new Revista
                        {
                            IdRevista = revista.IdRevista,
                            IdSeccion = revista.IdSeccion,
                            NombreRevista = revista.NombreRevista,
                            EstadoRevista = revista.EstadoRevista
                        };

                        revistaList.Add(revistals);
                    }

                    reply.ok = true;
                    reply.data = revistaList;

                    return Ok(reply);
                }
            }
            else if (r.IdRevista != 0 && r.IdSeccion != 0)
            {
                var revistas = await ctx.Revista.FirstOrDefaultAsync(e => e.IdRevista == r.IdRevista && e.IdSeccion == r.IdSeccion);

                if (revistas == null)
                {
                    reply.ok = false;
                    reply.data = "Revista no encontrada";

                }
                else
                {
                    Revista revistals = new Revista
                    {
                        IdRevista = revistas.IdRevista,
                        IdSeccion = revistas.IdSeccion,
                        NombreRevista = revistas.NombreRevista,
                        EstadoRevista = revistas.EstadoRevista
                    };

                    reply.ok = true;
                    reply.data = revistals;
                }
            }
            else if (r.IdRevista != 0 && r.IdSeccion == 0)
            {
                var listrevista = await ctx.Revista.Where(e => e.IdRevista == r.IdRevista).ToListAsync();
                List<Revista> seccList = new List<Revista>();

                if (listrevista.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese articulo registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var revistas in listrevista)
                    {
                        Revista seccionls = new Revista
                        {
                            IdRevista = revistas.IdRevista,
                            IdSeccion = revistas.IdSeccion,
                            NombreRevista = revistas.NombreRevista,
                            EstadoRevista = revistas.EstadoRevista
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


        
    }
}
