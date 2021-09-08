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
    public class SeccionesController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public SeccionesController(disconCTX _ctx) => ctx = _ctx;

        [HttpPost("Listar")]
        public async Task<IActionResult> Get (Secciones s)
        {
            if (s.IdSeccion == 0)
            {
                List<Secciones> listaSeccion = await ctx.Secciones.ToListAsync();
                List<Secciones> seccList = new List<Secciones>();

                foreach (var seccion in listaSeccion)
                {

                    Secciones seccLis = new Secciones
                    {
                        IdSeccion = seccion.IdSeccion,
                        NombreSeccion = seccion.NombreSeccion,
                        EstadoSeccion = seccion.EstadoSeccion
                    };

                    seccList.Add(seccLis);
                }

                reply.ok = true;
                reply.data = seccList;

                return Ok(reply);
            }
            else
            {
                var seccion = await ctx.Secciones.FirstOrDefaultAsync(e => e.IdSeccion == s.IdSeccion);

                if (seccion == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Secciones lsSeccion = new Secciones();
                    lsSeccion.IdSeccion = seccion.IdSeccion;
                    lsSeccion.NombreSeccion = seccion.NombreSeccion;
                    lsSeccion.EstadoSeccion = seccion.EstadoSeccion;


                    reply.ok = true;
                    reply.data = seccion;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(Secciones se)
        {
            try
            {
                var u = await ctx.Secciones.FirstOrDefaultAsync(e => e.NombreSeccion == se.NombreSeccion);
                //Insertar
                if (se.IdSeccion == 0 && u != null)//nombre existe
                {

                    reply.ok = false;
                    reply.data = "Nombre de seccion en uso";

                }

                else if (se.IdSeccion == 0 && u == null) //nombre NO existe
                {
                    ctx.Secciones.Add(se);

                    reply.ok = true;
                    reply.data = se;
                }
                //Actualizar
                else if (se.IdSeccion != 0 && u == null) //nombre NO existe
                {
                    var seccionName = await ctx.Secciones.FirstOrDefaultAsync(e => e.IdSeccion == se.IdSeccion);

                    seccionName.IdSeccion = se.IdSeccion;
                    seccionName.NombreSeccion = se.NombreSeccion;
                    seccionName.EstadoSeccion = se.EstadoSeccion;


                    ctx.Entry(seccionName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = se;
                }
                else if (se.IdSeccion != 0 && u != null && se.IdSeccion != u.IdSeccion) //nombre SI existe
                {
                    reply.ok = false;
                    reply.data = "Nombre de seccion en uso";

                }
                else if (se.IdSeccion != 0 && u != null && se.IdSeccion == u.IdSeccion) //nombre es el mismo que ya tenía
                {

                    var seccionName = await ctx.Secciones.FirstAsync(e => e.IdSeccion == u.IdSeccion);

                    seccionName.IdSeccion = se.IdSeccion;
                    seccionName.NombreSeccion = se.NombreSeccion;
                    seccionName.EstadoSeccion = se.EstadoSeccion;

                    ctx.Entry(seccionName).CurrentValues.SetValues(seccionName);

                    reply.ok = true;
                    reply.data = seccionName;

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
