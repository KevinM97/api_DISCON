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
    public class ImagenesObraController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ImagenesObraController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(ImagenesObra img)
        {
            if(img.IdImagenobra == 0)
            {
                {
                    List<ImagenesObra> listaImagenesObra = await ctx.ImagenesObra.ToListAsync();
                    List<ImagenesObra> imagenesObraList = new List<ImagenesObra>();

                    foreach (var imgObra in listaImagenesObra)
                    {

                        ImagenesObra imgObraLis = new ImagenesObra
                        {
                            IdImagenobra = imgObra.IdImagenobra,
                            UrlImagenobra = imgObra.UrlImagenobra
                        };

                        imagenesObraList.Add(imgObraLis);
                    }

                    reply.ok = true;
                    reply.data = imagenesObraList;

                    return Ok(reply);
                }
            }
            else
            {
                var imgObra = await ctx.ImagenesObra.FirstOrDefaultAsync(e => e.IdImagenobra == img.IdImagenobra);

                if (imgObra == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    ImagenesObra lsimgObra = new ImagenesObra();
                    lsimgObra.IdImagenobra = imgObra.IdImagenobra;
                    lsimgObra.UrlImagenobra = imgObra.UrlImagenobra;

                    reply.ok = true;
                    reply.data = imgObra;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(ImagenesObra imo)
        {
            try
            {
                //Por el momento, solo podra existir una imagen por obra,
                var u = await ctx.ImagenesObra.FirstOrDefaultAsync(e => e.UrlImagenobra == imo.UrlImagenobra);
                //Insertar
                if (imo.IdImagenobra == 0 && u != null)//url existe
                {

                    reply.ok = false;
                    reply.data = "Imagen en uso";

                }

                else if (imo.IdImagenobra == 0 && u == null) //url NO existe
                {
                    ctx.ImagenesObra.Add(imo);

                    reply.ok = true;
                    reply.data = imo;
                }
                //Actualizar
                else if (imo.IdImagenobra != 0 && u == null) //url NO existe
                {
                    var imag = await ctx.ImagenesObra.FirstOrDefaultAsync(e => e.IdImagenobra == imo.IdImagenobra);

                    imag.IdImagenobra = imo.IdImagenobra;
                    imag.UrlImagenobra = imo.UrlImagenobra;



                    ctx.Entry(imag).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = imo;
                }
                else if (imo.IdImagenobra != 0 && u != null && imo.IdImagenobra != u.IdImagenobra) //url SI existe
                {
                    reply.ok = false;
                    reply.data = "imagen en uso";

                }
                else if (imo.IdImagenobra != 0 && u != null && imo.IdImagenobra == u.IdImagenobra) //url es la mismo que ya tenía
                {

                    var imag = await ctx.ImagenesObra.FirstOrDefaultAsync(e => e.IdImagenobra == u.IdImagenobra);

                    imag.IdImagenobra = imo.IdImagenobra;
                    imag.UrlImagenobra = imo.UrlImagenobra;

                    ctx.Entry(imag).CurrentValues.SetValues(imag);

                    reply.ok = true;
                    reply.data = reply;

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
