using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface IUsuarioRepository : IRepository<ApplicationUser>
    {
        void BloquearUsuario(string idUsuario);
        void DesbloquearUsuario(string idUsuario);
    }
}
