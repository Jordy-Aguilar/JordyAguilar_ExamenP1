using System.ComponentModel.DataAnnotations;

namespace JordyAguilar_ExamenP1.Models
{
    public class JAguilar
    {
        public String Nombre {  get; set; }
        public float Salario {  get; set; }
        public int Id { get; set; }
        public String Descripcion { get; set; }
        public bool TieneHijos { get; set; }
        public DataType FechaNacimiento { get; set; }
    }
}
