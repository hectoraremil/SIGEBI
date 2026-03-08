namespace SIGEBI.Application.Dtos.RecursoBibliografico
{
    public class RecursoBibliograficoUpdateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public string Editorial { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
