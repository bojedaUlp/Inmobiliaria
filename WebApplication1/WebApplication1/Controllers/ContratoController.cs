using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContratoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioInmueble repositorioInmueble;
        private readonly RepositorioInquilino repositorioInquilino;
        private readonly RepositorioContrato repositorioContrato;

        public ContratoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioInmueble = new RepositorioInmueble(configuration);
            repositorioInquilino = new RepositorioInquilino(configuration);
            repositorioContrato = new RepositorioContrato(configuration);
        }



        // GET: Contrato
        public ActionResult Index()
        {
            var lista = repositorioContrato.ObtenerTodos();
            return View(lista);
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato c)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioContrato.Alta(c);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
                    ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
                ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            var c = repositorioContrato.ObtenerPorId(id);
            ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            return View(c);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato c)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioContrato.Modificacion(c);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
                    ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
                    return View(c);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
                ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
                ViewBag.Error = ex.Message;
                return View(c);
            }
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var c = repositorioContrato.ObtenerPorId(id);
                return View(c);
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato c)
        {
            try
            {
                // TODO: Add delete logic here
                int res = repositorioContrato.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}