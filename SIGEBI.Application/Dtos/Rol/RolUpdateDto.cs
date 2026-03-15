namespace SIGEBI.Application.Dtos.Rol
{
    public class RolUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int MaxPrestamos { get; set; }
        public int DiasPrestamoDefault { get; set; }
        public bool? PuedeRenovar { get; set; }
        public int? MaxRenovaciones { get; set; }
        public bool Activo { get; set; }
    }
}
