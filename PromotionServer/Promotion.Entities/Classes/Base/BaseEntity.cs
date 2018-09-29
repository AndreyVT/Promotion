using Promotion.Entities.Interfaces.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Promotion.Entities.Classes.Base
{
    public class BaseEntity: IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
