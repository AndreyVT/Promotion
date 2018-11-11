namespace Promotion.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BonusLimit")]
    public class PBonusLimit : BaseBusinesEntity
    {
        [ForeignKey(nameof(Bonus))]
        public int BonusId { get; set; }

        /// <summary>
        /// Конкретный бонус (Спасибка)
        /// </summary>
        public PBonus Bonus { get; set; }

        public long Limit { get; set; }

        [ForeignKey(nameof(Period))]
        public int PeriodId { get; set; }

        public PPeriod Period { get; set; }

        public bool IsOpened { get; set; }
    }
}
