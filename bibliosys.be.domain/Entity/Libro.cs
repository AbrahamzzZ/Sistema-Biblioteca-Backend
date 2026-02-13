namespace bibliosys.be.domain.Entity
{
    public class Libro
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string? Editorial { get; set; }
        public int? Anio_Publicacion { get; set; }
        public string? Genero { get; set; }
        public int Stock { get; set; }
        public string? Ubicacion { get; set; }
        public bool Estado { get; set; } = true; // BIT en BD
    }
}

