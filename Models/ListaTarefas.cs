using System.ComponentModel.DataAnnotations;

namespace Fattocs.Models
{
    public class ListaTarefas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da tarefa é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O custo é obrigatório.")]
        [Range(0, float.MaxValue, ErrorMessage = "O custo deve ser um valor positivo.")]
        public float Custo { get; set; }

        [Required(ErrorMessage = "A data limite é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataLimite { get; set; }

        public int? Ordenacao { get; set; }
    }
}
