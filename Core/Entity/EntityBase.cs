using Core.Enum;

namespace Core.Entity
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public StatusType Status { get; set; }
    }
}
