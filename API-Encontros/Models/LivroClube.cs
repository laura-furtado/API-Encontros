using System.ComponentModel.DataAnnotations;

namespace API_Encontros.Models
{
    public class LivroClube
    {
        [Key]
        public Guid id { get; set; }
        public Guid clube_id { get; set; }
        // demais propriedades serão adicionadas por outra pessoa
    }
}
