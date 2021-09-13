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
    public class ObrasController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ObrasController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Obras o)
        {
            if (o.IdObra == 0)
            {
                List<Obras> listaObras = await ctx.Obras.ToListAsync();
                List<Obras> obrasList = new List<Obras>();

                foreach (var obras in listaObras)
                {

                    Obras obrasLis = new Obras
                    {
                        IdObra = obras.IdObra,
                        IdImagenobra = obras.IdImagenobra,
                        TituloObra= obras.TituloObra,
                        EstadoObra = obras.EstadoObra,
                    };

                    obrasList.Add(obrasLis);
                }

                reply.ok = true;
                reply.data = obrasList;

                return Ok(reply);
            }
            else
            {
                var obras = await ctx.Obras.FirstOrDefaultAsync(e => e.IdObra == o.IdObra);

                if (obras == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Obras lsObras = new Obras();
                    lsObras.IdObra = obras.IdObra;
                    lsObras.IdImagenobra = obras.IdImagenobra;
                    lsObras.TituloObra = obras.TituloObra;
                    lsObras.EstadoObra = obras.EstadoObra;


                    reply.ok = true;
                    reply.data = obras;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]

        public async Task<IActionResult> Post(Obras ob)
        {
            try
            {
                var u = await ctx.Obras.FirstOrDefaultAsync(e => e.TituloObra == ob.TituloObra);
                //Insertar
                if (ob.IdObra == 0 && u != null)
                {

                    reply.ok = false;
                    reply.data = "Nombre de obra en uso";

                }

                else if (ob.IdObra == 0 && u == null)
                {
                    ctx.Obras.Add(ob);

                    reply.ok = true;
                    reply.data = ob;
                }
                //Actualizar
                else if (ob.IdObra != 0 && u == null)
                {
                    var obraName = await ctx.Obras.FirstOrDefaultAsync(e => e.IdObra == ob.IdObra);

                    obraName.IdObra = ob.IdObra;
                    obraName.IdImagenobra = ob.IdImagenobra;
                    obraName.TituloObra = ob.TituloObra;
                    obraName.EstadoObra = ob.EstadoObra;


                    ctx.Entry(obraName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = ob;
                }
                else if (ob.IdObra != 0 && u != null && ob.IdObra != u.IdObra)
                {
                    reply.ok = false;
                    reply.data = "Nombre de obra en uso";

                }
                else if (ob.IdObra != 0 && u != null && ob.IdObra == u.IdObra)
                {

                    var obraName = await ctx.Obras.FirstAsync(e => e.IdObra == u.IdObra);

                    obraName.IdObra = ob.IdObra;
                    obraName.IdImagenobra = ob.IdImagenobra;
                    obraName.TituloObra = ob.TituloObra;
                    obraName.EstadoObra = ob.EstadoObra;

                    ctx.Entry(obraName).CurrentValues.SetValues(obraName);

                    reply.ok = true;
                    reply.data = obraName;

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
