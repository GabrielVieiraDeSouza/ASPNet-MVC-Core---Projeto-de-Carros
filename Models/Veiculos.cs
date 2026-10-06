using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Carros.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Obrigatorio informar um nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatorio informar uma Placa")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "Obrigatorio informar um Ano de Fabricação")]
        [Display (Name = "Ano de Fabricação")]
        public int AnoFabricacao { get; set; }
        [Required(ErrorMessage = "Obrigatorio informar o ano do Modelo")]
        [Display(Name = "Ano do Modelo")]
        public int AnoModelo { get; set; }
    }
}
