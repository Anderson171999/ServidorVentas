using Microsoft.EntityFrameworkCore;
using SistemaVenta.Server.Models;
using SistemaVenta.Server.Repositorio.Contrato;
using System.Linq.Expressions;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;


namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly DBVentaContext _dbContext;

        public EventoRepositorio(DBVentaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<EVENTO>> Consultar(Expression<Func<EVENTO, bool>> filtro = null)
        {
            IQueryable<EVENTO> queryEntidad = filtro == null ? _dbContext.EVENTO : _dbContext.EVENTO.Where(filtro);
            return queryEntidad;
        }

        public async Task<EVENTO> Crear(EVENTO entidad)
        {
            try
            {
                _dbContext.Set<EVENTO>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<EVENTO> Obtener(Expression<Func<EVENTO, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.EVENTO.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Editar(EVENTO entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }







    }
}
