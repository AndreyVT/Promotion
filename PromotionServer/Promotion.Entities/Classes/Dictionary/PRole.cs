namespace Promotion.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Roles")]
    public class PRole: BaseDictionaryEntity
    {
        public string LogicalName { get; set; }
    }
}
