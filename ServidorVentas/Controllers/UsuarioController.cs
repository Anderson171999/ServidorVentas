using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Server.Models;
using SistemaVenta.Server.Repositorio.Contrato;
using SistemaVenta.Shared;


namespace SistemaVenta.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
        }



        [HttpGet]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(string usuario, string clave)
        {
            ResponseDTO<USUARIO> _ResponseDTO = new ResponseDTO<USUARIO>();
            try
            {
                USUARIO _usuario = await _usuarioRepositorio.Obtener(u => u.Usuario1 == usuario && u.Clave == clave);


                if (_usuario != null)
                    _ResponseDTO = new ResponseDTO<USUARIO>() { status = true, msg = "ok", value = _usuario };
                else
                    _ResponseDTO = new ResponseDTO<USUARIO>() { status = false, msg = "no encontrado", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<USUARIO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


    }
}