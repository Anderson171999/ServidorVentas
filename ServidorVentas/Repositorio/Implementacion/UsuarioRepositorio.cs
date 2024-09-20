using Microsoft.EntityFrameworkCore;
using SistemaVenta.Server.Models;
using SistemaVenta.Server.Repositorio.Contrato;
using System.Linq.Expressions;


namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DBVentaContext _context;

        public UsuarioRepositorio(DBVentaContext context)
        {
            _context = context;
        }


        public async Task<USUARIO> Obtener(Expression<Func<USUARIO, bool>> filtro = null)
        {
            try
            {
                return await _context.USUARIO.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }


    }
}
