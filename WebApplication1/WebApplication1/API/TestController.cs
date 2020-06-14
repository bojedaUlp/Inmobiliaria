using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DataContext context;
        public TestController(DataContext context) {
            this.context = context;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(new
                {
                    Mensaje = "Éxito",
                    Error = 0,
                    Resultado = new
                    {
                        Clave = "Key",
                        Valor = "Value"
                    },
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public ActionResult<Propietario> Get(int id)
        {
            if (id <= 0) {
                return NotFound();
            }
            else
            {
                return context.Propietario.First(x => x.Id_Propietario == id);
            }
            
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
