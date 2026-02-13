namespace bibliosys.be.domain.Entity
{
    public class Bibliotecario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Estado { get; set; } = true; // BIT en BD
    }
}

