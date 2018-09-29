namespace Promotion.Entities.Busines
{
    using Promotion.Entities.Classes.Base;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class PUser: BaseBusinesEntity
    {
        public string ExternalId { get; set; }
        public string FullName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
