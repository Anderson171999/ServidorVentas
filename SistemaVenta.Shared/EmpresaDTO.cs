
namespace SistemaVenta.Shared
{
    public class EventoDTO
    {
        public int IdEvento { get; set; }
        public DateTime FechaEvento { get; set; }

        public string? LugarEvento { get; set; }
        public string? DescripcionEvento { get; set; }

        public string? Precio { get; set; }
        public bool? Estado { get; set; }

    }
}
