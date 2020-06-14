using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class InmuebleController : ControllerBase
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public InmuebleController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        // GET: api/Inmuebles
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
              
                return Ok(contexto.Inmueble.Include(e => e.Propietario).Where(e => e.Propietario.EmailP == usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // GET: api/Inmuebles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.Inmueble.Include(e => e.Propietario).Where(e => e.Propietario.EmailP == usuario).Single(e => e.Id_Inmueble == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        // POST: api/Inmuebles
        [HttpPost]
        public async Task<IActionResult> Post(Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.Id_Propietario = contexto.Propietario.Single(e => e.EmailP == User.Identity.Name).Id_Propietario;
                    contexto.Inmueble.Add(entidad);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = entidad.Id_Inmueble }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Inmuebles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.Inmueble.AsNoTracking().Include(e => e.Propietario).FirstOrDefault(e => e.Id_Inmueble == id && e.Propietario.EmailP == User.Identity.Name) != null)
                {
                    entidad.Id_Inmueble = id;
                    contexto.Inmueble.Update(entidad);
                    contexto.SaveChanges();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entidad = contexto.Inmueble.Include(e => e.Propietario).FirstOrDefault(e => e.Id_Inmueble == id && e.Propietario.EmailP == User.Identity.Name);
                if (entidad != null)
                {
                    contexto.Inmueble.Remove(entidad);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("BajaLogica/{id}")]
        public async Task<IActionResult> BajaLogica(int id)
        {
            try
            {
                var entidad = contexto.Inmueble.Include(e => e.Propietario).FirstOrDefault(e => e.Id_Inmueble == id && e.Propietario.EmailP == User.Identity.Name);
                if (entidad != null)
                {
                    entidad.DireccionInm = "-1";//cambiar por estado = 0
                    contexto.Inmueble.Update(entidad);
                    contexto.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
