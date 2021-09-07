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
    public class ProductosController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ProductosController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Productos p)
        {
            if (p.IdProducto == 0)
            {
                List<Productos> listaProd = await ctx.Productos.ToListAsync();
                List<Productos> ProdList = new List<Productos>();

                foreach (var productos in listaProd)
                {

                    Productos prodLis = new Productos
                    {
                        IdProducto = productos.IdProducto,
                        NombreProducto = productos.NombreProducto,
                        PrecioProducto = productos.PrecioProducto,
                        DescripcionProd = productos.DescripcionProd

                    };

                    ProdList.Add(prodLis);
                }

                reply.ok = true;
                reply.data = ProdList;

                return Ok(reply);
            }
            else
            {
                var productos = await ctx.Productos.FirstOrDefaultAsync(e => e.IdProducto == p.IdProducto);

                if (productos == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Productos lsProd = new Productos();
                    lsProd.IdProducto = productos.IdProducto;
                    lsProd.NombreProducto = productos.NombreProducto;
                    lsProd.PrecioProducto = productos.PrecioProducto;
                    lsProd.DescripcionProd = productos.DescripcionProd;

                    reply.ok = true;
                    reply.data = productos;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] Productos pr)
        {
            try
            {
                var u = await ctx.Productos.FirstOrDefaultAsync(e => e.NombreProducto == pr.NombreProducto);
                //Insertar
                if (pr.IdProducto == 0 && u != null)//nombre existe
                {

                    reply.ok = false;
                    reply.data = "Producto ya registrado";

                }

                else if (pr.IdProducto == 0 && u == null) //nombre NO existe
                {
                    ctx.Productos.Add(pr);

                    reply.ok = true;
                    reply.data = pr;
                }
                //Actualizar
                else if (pr.IdProducto != 0 && u == null) //nombre NO existe
                {
                    var productoName = await ctx.Productos.FirstOrDefaultAsync(e => e.IdProducto == pr.IdProducto);

                    productoName.IdProducto = pr.IdProducto;
                    productoName.NombreProducto = pr.NombreProducto;
                    productoName.PrecioProducto = pr.PrecioProducto;
                    productoName.DescripcionProd = pr.DescripcionProd;



                    ctx.Entry(productoName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = pr;
                }
                else if (pr.IdProducto != 0 && u != null && pr.IdProducto != u.IdProducto) //nombre SI existe
                {
                    reply.ok = false;
                    reply.data = "Producto ya registrado";

                }
                else if (pr.IdProducto != 0 && u != null && pr.IdProducto == u.IdProducto) //nombre es el mismo que ya tenía
                {

                    var productoName = await ctx.Productos.FirstAsync(e => e.IdProducto == u.IdProducto);

                    productoName.IdProducto = pr.IdProducto;
                    productoName.NombreProducto = pr.NombreProducto;
                    productoName.PrecioProducto = pr.PrecioProducto;
                    productoName.DescripcionProd = pr.DescripcionProd;

                    ctx.Entry(productoName).CurrentValues.SetValues(productoName);

                    reply.ok = true;
                    reply.data = productoName;

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
