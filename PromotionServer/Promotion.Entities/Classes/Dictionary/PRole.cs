namespace Promotion.Entities.Dictionary
{
    using Promotion.Entities.Classes.Base;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Roles")]
    public class PRole: BaseDictionaryEntity
    {
        public string LogicalName { get; set; }
    }
}
