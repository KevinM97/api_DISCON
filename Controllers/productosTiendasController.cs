using api_DISCON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Controllers
{
    public class productosTiendasController
    {
        [ApiController]
        [Route("api/[controller]")]
        public class TiendasController : ControllerBase
        {
            private readonly disconCTX ctx;
            Respuesta reply = new Respuesta();

            public TiendasController(disconCTX _ctx)
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
                            IdTienda = tiendas.IdTienda,
                            IdProducto = tiendas.IdProducto,
                        };

                        reply.ok = true;
                        reply.data = tiendals;
                    }
                }
                else if (ts.IdTienda != 0 && ts.IdProducto == 0)
                {
                    var listTiendas = await ctx.Tiendas.Where(e => e.IdTienda == ts.IdTienda).ToListAsync();
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
                                IdTienda = tienda.IdTienda,
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
                var p = await ctx.ProductosTiendas.Where(x => x.IdTienda == t.IdTienda && x.IdProducto == t.IdProducto).FirstOrDefaultAsync();
                var validarIdProd = await ctx.ProductosTiendas.FirstOrDefaultAsync(x => x.IdTienda == t.IdTienda && x.IdProducto == t.IdProducto);

                if (t.IdTienda == 0 && p == null)
                {
                    ProductosTiendas tienda = new ProductosTiendas();
                    tienda.IdTienda = t.IdTienda;

                    if (validarIdProd == null)
                    {
                        tienda.IdProducto = t.IdProducto;
                    }
                    else
                    {
                        reply.ok = false;
                        reply.data = "El producto ya existe en la tienda";

                        return Ok(reply);
                    }



                    ctx.ProductosTiendas.Add(tienda);

                    reply.ok = true;
                    reply.data = tienda;
                }
                else
                {
                    var validarTienda = await ctx.ProductosTiendas.Where(x => x.IdTienda == t.IdTienda && x.IdProducto == t.IdProducto).FirstOrDefaultAsync();

                    if (validarIdProd == null)
                    {
                        p.IdProducto = t.IdProducto;
                    }
                    else if (validarTienda == null)
                    {
                        reply.ok = false;
                        reply.data = "El producto ya existe en la tienda";

                        return Ok(reply);
                    }

                    p.IdProducto = t.IdProducto;
                    p.IdTienda = t.IdTienda;

                    reply.ok = true;
                    reply.data = p;

                    ctx.Entry(p).State = EntityState.Modified;


                }

                await ctx.SaveChangesAsync();
                return Ok(reply);

            }
        }
    }
}
