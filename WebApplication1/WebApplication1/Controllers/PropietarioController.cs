using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioPropietario repositorioPropietario;
        private readonly RepositorioUsuario repositorioUsuario;

        public PropietarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioPropietario = new RepositorioPropietario(configuration);
            repositorioUsuario = new RepositorioUsuario(configuration);
        }

        // GET: Propietario
        [Authorize(Policy = "Administrador")]
        public ActionResult Index()
        {
            
            var lista = repositorioPropietario.ObtenerTodos();
            return View(lista);
        }

        // GET: Propietario/Details/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Propietario/Create
        [Authorize(Policy = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        {
           
            try
            {
                if(ModelState.IsValid)
                {
                    p.ClaveP = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                       password: p.ClaveP,
                       salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                       prf: KeyDerivationPrf.HMACSHA1,
                       iterationCount: 1000,
                       numBytesRequested: 256 / 8));
                    int res = repositorioPropietario.Alta(p);
                    TempData["Id_Propietario"] = p.Id_Propietario;                 
                    return RedirectToAction(nameof(Index));                    
                }
                else { return View(); }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Propietario/Edit/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Edit(int id)
        {
            try
            {
                Propietario p = repositorioPropietario.ObtenerPorId(id);
                return View(p);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Edit(int id, Propietario p)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioPropietario.Modificacion(p);
                    return RedirectToAction(nameof(Index));
                }
                else { return View(); }
             
            }
            catch(Exception ex)
            {
                ViewBag.Error(ex.Message);
                return View();
            }
        }

        // GET: Propietario/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            try
            {         
                 Propietario p = repositorioPropietario.ObtenerPorId(id);
                 return View(p);
               
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Propietario p)
        {
            try
            {
                // TODO: Add delete logic here
                if(ModelState.IsValid)
                {
                    int res = repositorioPropietario.Baja(id);
                    return RedirectToAction(nameof(Index));
                }
                else { return View(); }
                
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}