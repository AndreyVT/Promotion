namespace Promotion.Entities.Classes.DataEntities
{
    using Promotion.Entities.Busines;
    using Promotion.Entities.Classes.Base;
    using Promotion.Entities.Classes.Dictionary;
    using Promotion.Entities.Dictionary;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BonusTransactions")]
    public class PBonusTransactions: BaseDescriptionEntity
    {
        [ForeignKey(nameof(UserDonator))]
        public int? UserDOnatorId { get; set; }

        public PUser UserDonator { get; set; }

        [ForeignKey(nameof(UserRecipient))]
        public int? UserRecipientId { get; set; }

        public PUser UserRecipient { get; set; }

        [ForeignKey(nameof(Period))]
        public int? PeriodId { get; set; }

        public PPeriod Period { get; set; }

        [ForeignKey(nameof(Bonus))]
        public int? BonusId { get; set; }

        public PBonus Bonus { get; set; }

        public long Value { get; set; }

        public bool IsDeleted { get; set; }
    }
}
