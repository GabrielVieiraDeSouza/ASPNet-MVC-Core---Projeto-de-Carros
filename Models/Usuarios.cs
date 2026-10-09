using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Carros.Models
{
    public class Usuarios
    {
        [Key]
        public int ID { get; set; }

        [Required( ErrorMessage = "Obrigatorio informar o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar uma senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Perfil Perfil { get; set; }
    }

    public enum Perfil
    {
        Admin,
        User
    }
}
