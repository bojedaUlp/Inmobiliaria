using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PagoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioPago repositorioPago;
        private readonly RepositorioContrato repositorioContrato;
        private readonly RepositorioInquilino repositorioInquilino;

        public PagoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioPago = new RepositorioPago(configuration);
            repositorioContrato = new RepositorioContrato(configuration);
            repositorioInquilino = new RepositorioInquilino(configuration);
        }
        // GET: Pago
        public ActionResult Index()
        {
            try {
                var lista = repositorioPago.ObtenerTodos();
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult PorPago(int id)
        {
            try
            {
                var lista = repositorioPago.ObtenerPorIdInquilino(id);
                ViewBag.id_Inquilino = id;
                ViewBag.Inquilino = repositorioInquilino.ObtenerPorId(id);
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                return View();
            }
            catch (Exception ex) {
                ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago p)
        {
            try
            {
                // TODO: Add insert logic here
                int res = repositorioPago.Alta(p);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            try {
                
                Pago p = repositorioPago.ObtenerPorId(id);
                ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                return View(p);
            } 
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                return View();

            }
            
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago p)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioPago.Modificacion(p);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                    return View();
                }
            }
            catch(Exception ex)
            {

                ViewBag.Error = ex.Message;
                ViewBag.Contratos = repositorioContrato.ObtenerTodos();
                return View();
            }
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var p = repositorioPago.ObtenerPorId(id);
                return View(p);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago p )
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioPago.Baja(id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}