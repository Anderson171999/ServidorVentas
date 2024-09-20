using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Server.Models;
using SistemaVenta.Server.Repositorio.Contrato;
using SistemaVenta.Shared;
using SistemaVentaBlazor.Server.Repositorio.Implementacion;


namespace SistemaVenta.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IEventoRepositorio _eventoRepositorio;
        public EventoController(IEventoRepositorio eventoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _eventoRepositorio = eventoRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<EventoDTO>> _ResponseDTO = new ResponseDTO<List<EventoDTO>>();

            try
            {
                List<EventoDTO> ListaEventos = new List<EventoDTO>();
                IQueryable<EVENTO> query = await _eventoRepositorio.Consultar();

                ListaEventos = _mapper.Map<List<EventoDTO>>(query.ToList());

                if (ListaEventos.Count >= 0)
                    _ResponseDTO = new ResponseDTO<List<EventoDTO>>() { status = true, msg = "ok", value = ListaEventos };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<EventoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Guardar([FromBody] EventoDTO request)
        {
            ResponseDTO<EventoDTO> _ResponseDTO = new ResponseDTO<EventoDTO>();
            try
            {
                EVENTO _evento = _mapper.Map<EVENTO>(request);

                EVENTO _eventoCreado = await _eventoRepositorio.Crear(_evento);

                if (_eventoCreado.IdEvento != 0)
                    _ResponseDTO = new ResponseDTO<EventoDTO>() { status = true, msg = "ok", value = _mapper.Map<EventoDTO>(_eventoCreado) };
                else
                    _ResponseDTO = new ResponseDTO<EventoDTO>() { status = false, msg = "No se pudo crear el evento" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<EventoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] EventoDTO request)
        {
            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();
            try
            {
                EVENTO _evento = _mapper.Map<EVENTO>(request);
                EVENTO _eventoParaEditar = await _eventoRepositorio.Obtener(u => u.IdEvento == _evento.IdEvento);
                _eventoParaEditar.FechaEvento = request.FechaEvento;

                if (_eventoParaEditar != null)
                {
                    _eventoParaEditar.FechaEvento = _eventoParaEditar.FechaEvento;
                    _eventoParaEditar.LugarEvento = _evento.LugarEvento;
                    _eventoParaEditar.DescripcionEvento = _evento.DescripcionEvento;
                    _eventoParaEditar.Precio = _evento.Precio;

                    bool respuesta = await _eventoRepositorio.Editar(_eventoParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo editar el evento" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el evento" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }



        [HttpPut("{id}/actualizar-estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EventoDTO request)
        {
            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();
            try
            {


                EVENTO _eventoParaActualizar = await _eventoRepositorio.Obtener(u => u.IdEvento == id);

                if (_eventoParaActualizar != null)
                {
                    _eventoParaActualizar.Estado = request.Estado;

                    bool respuesta = await _eventoRepositorio.Editar(_eventoParaActualizar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo actualizar el estado del evento" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el evento" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }





    }
}
