using System.ComponentModel.DataAnnotations;

namespace API_Encontros
{
    public class Clube
    {
        [Key]
        public Guid id { get; set; }
        // demais propriedades serão adicionadas por outra pessoa
    }
}
