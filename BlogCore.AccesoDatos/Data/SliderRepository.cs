using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BlogCore.AccesoDatos.Data.Repository;
using System.Linq;

namespace BlogCore.AccesoDatos.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(m => m.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;
            _db.SaveChanges();
        }
    }
}
