using Domain.Enum;

namespace Domain.DTO
{
    public class FilmeDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public EnumClassificacaoIndicativa ClassificacaoIndicativa { get; set; }
    }
}
