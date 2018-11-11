namespace Promotion.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity: IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
