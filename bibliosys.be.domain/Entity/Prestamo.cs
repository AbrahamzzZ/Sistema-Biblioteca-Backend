using System;

namespace bibliosys.be.domain.Entity
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public int Libro_Id { get; set; }
        public DateTime? Fecha_Prestamo { get; set; }
        public DateTime Fecha_Limite_Devolucion { get; set; }
        public DateTime? Fecha_Real_Devolucion { get; set; }
        public string? Observacion { get; set; }
        public bool Estado { get; set; } = true; // 1: Prestado, 0: Devuelto

        public Usuario? Usuario { get; set; }
        public Libro? Libro { get; set; }
    }
}

