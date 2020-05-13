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
    public class InquilinoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioInquilino repositorioInquilino;

        public InquilinoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioInquilino = new RepositorioInquilino(configuration);
        }

        // GET: Inquilino
        public ActionResult Index()
        {
            var lista = repositorioInquilino.ObtenerTodos();
            return View(lista);
        }

        // GET: Inquilino/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inquilino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino i)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioInquilino.Alta(i);
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

        // GET: Inquilino/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Inquilino i = repositorioInquilino.ObtenerPorId(id);
                return View(i);
            }catch(Exception ex)
            {
                ViewBag.Eror = ex.Message;
                return View();
            }
        }

        // POST: Inquilino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino i)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioInquilino.Modificacion(i);
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

        // GET: Inquilino/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

                Inquilino i = repositorioInquilino.ObtenerPorId(id);
                return View(i);
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
        }

        // POST: Inquilino/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino i)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    int res = repositorioInquilino.Baja(id);
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