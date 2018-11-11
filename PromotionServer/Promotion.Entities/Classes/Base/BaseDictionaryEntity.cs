namespace Promotion.Domain.Entities
{
    public class BaseDictionaryEntity: BaseDescriptionEntity, IBaseDictionaryEntity
    {
        public string Name { get; set; }
    }
}
