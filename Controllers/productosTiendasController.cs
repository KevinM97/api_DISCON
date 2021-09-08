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
    public class ProductosTiendasController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ProductosTiendasController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(ProductosTiendas ts)
        {
            if (ts.IdTienda == 0 && ts.IdProducto == 0)
            {
                List<ProductosTiendas> tiendasList = await ctx.ProductosTiendas.ToListAsync();
                List<ProductosTiendas> listTiendas = new List<ProductosTiendas>();
                foreach (var tienda in tiendasList)
                {
                    ProductosTiendas tiendasls = new ProductosTiendas
                    {
                        IdProdtiend = tienda.IdProdtiend,
                        IdTienda = tienda.IdTienda,
                        IdProducto = tienda.IdProducto,
                    };

                    listTiendas.Add(tiendasls);
                }

                reply.ok = true;
                reply.data = listTiendas;
            }
            else if (ts.IdTienda == 0 && ts.IdProducto != 0)
            {
                var listTiendas = await ctx.ProductosTiendas.Where(e => e.IdProducto == ts.IdProducto).ToListAsync();
                List<ProductosTiendas> tiendaList = new List<ProductosTiendas>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese producto registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        ProductosTiendas tiendasls = new ProductosTiendas
                        {
                            IdProdtiend = tienda.IdProdtiend,
                            IdTienda = tienda.IdTienda,
                            IdProducto = tienda.IdProducto,
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            else if (ts.IdTienda != 0 && ts.IdProducto != 0)
            {
                var tiendas = await ctx.ProductosTiendas.FirstOrDefaultAsync(e => e.IdTienda == ts.IdTienda && e.IdProducto == ts.IdProducto);

                if (tiendas == null)
                {
                    reply.ok = false;
                    reply.data = "Tienda no encontrada";

                }
                else
                {
                    ProductosTiendas tiendals = new ProductosTiendas
                    {
                        IdProdtiend = tiendas.IdProdtiend,
                        IdTienda = tiendas.IdTienda,
                        IdProducto = tiendas.IdProducto,
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            else if (ts.IdTienda != 0 && ts.IdProducto == 0)
            {
                var listTiendas = await ctx.ProductosTiendas.Where(e => e.IdTienda == ts.IdTienda).ToListAsync();
                List<ProductosTiendas> tiendaList = new List<ProductosTiendas>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese producto registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        ProductosTiendas tiendasls = new ProductosTiendas
                        {
                            IdProdtiend = tienda.IdProdtiend,
                            IdTienda = tienda.IdTienda,
                            IdProducto = tienda.IdProducto
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
        public async Task<IActionResult> Post([FromBody] ProductosTiendas t)
        {
            var tiendas = await ctx.ProductosTiendas.FirstOrDefaultAsync(e => e.IdTienda == t.IdTienda);
            var producto = await ctx.ProductosTiendas.FirstOrDefaultAsync(e => e.IdProducto == t.IdProducto);
            if (t.IdProdtiend == 0 && t.IdTienda != 0 && t.IdProducto != 0)
            {
                if ( tiendas == null)
                {
                    reply.ok = false;
                    reply.data = "No existe esa tienda";

                    return Ok(reply);

                }else if ( producto == null)
                {
                    reply.ok = false;
                    reply.data = "No existe ese producto";

                    return Ok(reply);
                }
                else
                {
                    ctx.ProductosTiendas.Add(t);
                    reply.ok = true;
                    reply.data = t;
                }
            await ctx.SaveChangesAsync();
            }
            else if (t.IdProdtiend != 0 && t.IdTienda != 0 && t.IdProducto != 0)
            {
            var tienprod = await ctx.ProductosTiendas.FirstOrDefaultAsync(e => e.IdProdtiend == t.IdProdtiend);
                if (tienprod == null)
                {
                    reply.ok = false;
                    reply.data = "No existe ese elemento";

                    return Ok(reply);
                }
                else
                {
                    tienprod.IdProdtiend = t.IdProdtiend;
                    tienprod.IdTienda = t.IdTienda;
                    tienprod.IdProducto = t.IdProducto;

                    ctx.Entry(tienprod).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = t;
                }
            }
            await ctx.SaveChangesAsync();
            return Ok(reply);
        }
    }
}
