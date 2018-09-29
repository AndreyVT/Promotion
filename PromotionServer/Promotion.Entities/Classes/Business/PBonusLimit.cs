namespace Promotion.Entities.Busines
{
    using Promotion.Entities.Classes.Base;
    using Promotion.Entities.Classes.Dictionary;
    using Promotion.Entities.Dictionary;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BonusLimit")]
    public class PBonusLimit: BaseBusinesEntity
    {
        [ForeignKey(nameof(Bonus))]
        public int BonusId { get; set; }

        public PBonus Bonus { get; set; }

        public long Limit { get; set; }

        [ForeignKey(nameof(Period))]
        public int PeriodId { get; set; }

        public PPeriod Period { get; set; }
    }
}
