namespace Promotion.Entities.Classes.Base
{
    using Promotion.Entities.Interfaces.Interfaces;

    public class BaseDictionaryEntity: BaseDescriptionEntity, IBaseDictionaryEntity
    {
        public string Name { get; set; }
    }
}
