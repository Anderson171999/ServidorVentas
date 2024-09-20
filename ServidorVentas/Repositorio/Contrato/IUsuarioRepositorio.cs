using SistemaVenta.Server.Models;

using System.Linq.Expressions;

namespace SistemaVenta.Server.Repositorio.Contrato
{
    public interface IUsuarioRepositorio
    {
        Task<USUARIO> Obtener(Expression<Func<USUARIO, bool>> filtro = null);


    }

}
