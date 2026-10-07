using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Carros.Models
{
    [Table("Consumos")]
    public class Consumo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a descrição")]
        [Display (Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required( ErrorMessage = "Obrigatorio informar a data")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Necessário informar o valor")]
        public Decimal? Valor { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar a quilometragem")]
        public int? Km { get; set; }

        [Display (Name = "Tipo de Combustível")]
        public TipoCombustivel Tipo { get; set; }

        [Display (Name = "Veículo")]
        [Required(ErrorMessage = "Obrigatorio informar o Veículo")]
        public int VeiculoId { get; set; }


        [ForeignKey("VeiculoId")]
        public Veiculo Veiculo { get; set; }

    }

    public enum TipoCombustivel
    {
        Gasolina, 
        Etanol
    }
}
