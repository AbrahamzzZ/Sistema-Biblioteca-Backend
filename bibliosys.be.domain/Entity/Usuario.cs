namespace bibliosys.be.domain.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre_Completo { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo_Electronico { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool Estado { get; set; } = true; // BIT en BD
    }
}

