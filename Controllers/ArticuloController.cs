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
    public class ArticuloController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ArticuloController(disconCTX _ctx) => ctx = _ctx;


        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Articulo a)
        {
            if (a.IdArticulo == 0)
            {
                List<Articulo> listaArticulo = await ctx.Articulo.ToListAsync();
                List<Articulo> articuloList = new List<Articulo>();

                foreach (var articulo in listaArticulo)
                {

                    Articulo artiLis = new Articulo
                    {
                        IdArticulo = articulo.IdArticulo,
                        TituloArticulo = articulo.TituloArticulo,
                        ImagenArticulo = articulo.ImagenArticulo,
                        TextoArticulo = articulo.TextoArticulo,
                        EstadoArticulo = articulo.EstadoArticulo,

                    };

                    articuloList.Add(artiLis);
                }

                reply.ok = true;
                reply.data = articuloList;

                return Ok(reply);
            }
            else
            {
                var articulo = await ctx.Articulo.FirstOrDefaultAsync(e => e.IdArticulo == a.IdArticulo);

                if (articulo == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Articulo lsArticulo = new Articulo();
                    lsArticulo.IdArticulo = articulo.IdArticulo;
                    lsArticulo.TituloArticulo = articulo.TituloArticulo;
                    lsArticulo.ImagenArticulo = articulo.ImagenArticulo;
                    lsArticulo.TextoArticulo = articulo.TextoArticulo;
                    lsArticulo.EstadoArticulo = articulo.EstadoArticulo;


                    reply.ok = true;
                    reply.data = articulo;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] Articulo ar)
        {
            try
            {
                var u = await ctx.Articulo.FirstOrDefaultAsync(e => e.TituloArticulo == ar.TituloArticulo);
                //Insertar
                if (ar.IdArticulo == 0 && u != null)//nombre existe
                {

                    reply.ok = false;
                    reply.data = "Nombre de articulo en uso";

                }

                else if (ar.IdArticulo == 0 && u == null) //nombre NO existe
                {
                    ctx.Articulo.Add(ar);

                    reply.ok = true;
                    reply.data = ar;
                }
                //Actualizar
                else if (ar.IdArticulo != 0 && u == null) 
                {
                    var artName = await ctx.Articulo.FirstOrDefaultAsync(e => e.IdArticulo == ar.IdArticulo);

                    artName.IdArticulo = ar.IdArticulo;
                    artName.TituloArticulo = ar.TituloArticulo;
                    artName.ImagenArticulo = ar.ImagenArticulo;
                    artName.TextoArticulo = ar.TextoArticulo;
                    artName.EstadoArticulo = ar.EstadoArticulo;


                    ctx.Entry(artName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = ar;
                }
                else if (ar.IdArticulo != 0 && u != null && ar.IdArticulo != u.IdArticulo) 
                {
                    reply.ok = false;
                    reply.data = "Nombre de articulo en uso";

                }
                else if (ar.IdArticulo != 0 && u != null && ar.IdArticulo == u.IdArticulo) 
                {

                    var artName = await ctx.Articulo.FirstAsync(e => e.IdArticulo == u.IdArticulo);

                    artName.IdArticulo = ar.IdArticulo;
                    artName.TituloArticulo = ar.TituloArticulo;
                    artName.ImagenArticulo = ar.ImagenArticulo;
                    artName.TextoArticulo = ar.TextoArticulo;
                    artName.EstadoArticulo = ar.EstadoArticulo;

                    ctx.Entry(artName).CurrentValues.SetValues(artName);

                    reply.ok = true;
                    reply.data = artName;

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
