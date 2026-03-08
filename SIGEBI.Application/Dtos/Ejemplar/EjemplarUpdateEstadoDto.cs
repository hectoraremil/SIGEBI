using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Ejemplar
{
    public class EjemplarUpdateEstadoDto
    {
        public int Id { get; set; }
        public EstadoEjemplar Estado { get; set; }
    }
}
