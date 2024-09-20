using Microsoft.AspNetCore.Mvc;
using SistemaVenta.Server.Models;
using SistemaVenta.Shared;
using System.Linq.Expressions;

namespace SistemaVenta.Server.Repositorio.Contrato
{
    public interface IEventoRepositorio
    {
        Task<IQueryable<EVENTO>> Consultar(Expression<Func<EVENTO, bool>> filtro = null);
        Task<EVENTO> Crear(EVENTO entidad);
        Task<EVENTO> Obtener(Expression<Func<EVENTO, bool>> filtro = null);
        Task<bool> Editar(EVENTO entidad);

    }
}
