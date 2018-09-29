namespace Promotion.Entities.Classes.Base
{
    using Promotion.Entities.Interfaces.Interfaces;

    public class BaseBusinesEntity: BaseEntity, IBaseBusinesEntity
    {
        public string Name { get; set; }
    }
}
