using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {           
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                //ruta a donde se guardara
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //archivo antes de subirse
                var archivos = HttpContext.Request.Form.Files;

                //Nombre con el que se guardara
                string nombreArchivo = Guid.NewGuid().ToString();

                var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                //tipo de dato en este caso png o relativo a img
                var extension = Path.GetExtension(archivos[0].FileName);
                //Creamos el archivo en el proyecto
                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                //guardamos la urlimg en la db
                slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                _contenedorTrabajo.Slider.Add(slider);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                var slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                var sliderDesdeDb = _contenedorTrabajo.Slider.Get(slider.Id);
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if(archivos.Count() > 0)
                {
                    
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDb.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + nuevaExtension;
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderDesdeDb.UrlImagen;
                }
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {            
            var sliderDesdeDb = _contenedorTrabajo.Slider.Get(id);
            if (sliderDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando articulo" });
            }

            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, sliderDesdeDb.UrlImagen.TrimStart('\\'));                       
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            _contenedorTrabajo.Slider.Remove(sliderDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider borrado con exito" });
        }


        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        #endregion
    }
}
