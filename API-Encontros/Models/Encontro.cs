using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace API_Encontros.Models
{
    public class Encontro
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string titulo { get; set; }
        public string? descricao { get; set; }
        [Required]
        public DateTime data_hora { get; set; }
        public string? local { get; set; }
        public string? link_encontro { get; set; }
        public situacao situacao { get; set; } = situacao.AGENDADO;

        public DateTime criado_em { get; set; }
        
        public DateTime atualizado_em { get; set; }
        [Required]
        public Guid clube_id { get; set; }
        public Clube Clube { get; set; }

        public Guid? livro_clube_id { get; set; } 
        public LivroClube? LivroClube { get; set; }
    }
    public enum situacao {
        AGENDADO,
        REALIZADO,
        CANCELADO
    } 
}
