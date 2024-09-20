using AutoMapper;
using SistemaVenta.Server.Models;
using SistemaVenta.Shared;
using System.Globalization;

namespace SistemaVenta.Server.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Evento
            CreateMap<EVENTO, EventoDTO>().ReverseMap();
            #endregion Evento

            #region Usuario
            CreateMap<USUARIO, UsuarioDTO>().ReverseMap();
            #endregion Usuario

        }
    }
}
