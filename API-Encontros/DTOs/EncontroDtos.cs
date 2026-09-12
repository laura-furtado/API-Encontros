using API_Encontros.Models;
using System.ComponentModel.DataAnnotations;

namespace API_Encontros.DTOs
{
    //analisar se o usuário é o organizador
    public class CriarEncontroRequest
    {
        [Required]
        public Guid clube_id { get; set; }

        public Guid? livro_clube_id { get; set; }

        [Required]
        [StringLength(150)]
        public string titulo { get; set; } = string.Empty;

        public string? descricao { get; set; }

        [Required]
        public DateTime data_hora { get; set; }

        [StringLength(255)]
        public string? local { get; set; }

        [StringLength(500)]
        public string? link_encontro { get; set; }
    }

    public class AtualizarEncontroRequest
    {
        public Guid? livro_clube_id { get; set; }

        [Required]
        [StringLength(150)]
        public string titulo { get; set; } = string.Empty;

        public string? descricao { get; set; }

        [Required]
        public DateTime data_hora { get; set; }

        [StringLength(255)]
        public string? local { get; set; }

        [StringLength(500)]
        public string? link_encontro { get; set; }
    }

    public class AlterarSituacaoEncontroRequest
    {
        [Required]
        public situacao nova_situacao { get; set; }
    }


    public class EncontroResponse
    {
        public Guid id { get; set; }
        public Guid clube_id { get; set; }
        public Guid? livro_clube_id { get; set; }
        public string? descricao { get; set; }
        public DateTime data_hora { get; set; }
        public string? local { get; set; }
        public string? link_encontro { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string situacao { get; set; } = string.Empty;
        public DateTime criado_em { get; set; }
        public DateTime atualizado_em { get; set; }
    }


}
