using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities
{
    public class RecursoBibliografico : AuditableEntity<int>
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public string Editorial { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
    }
}
