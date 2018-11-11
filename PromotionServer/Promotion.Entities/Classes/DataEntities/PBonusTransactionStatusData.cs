namespace Promotion.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BonusTransactionStatusData")]
    public class PBonusTransactionStatusData: BaseDescriptionEntity
    {
        [ForeignKey(nameof(BonusTransaction))]
        public int BonusTransactionId { get; set; }

        public PBonusTransactions BonusTransaction { get; set; }

        [ForeignKey(nameof(BonusTransactionStatus))]
        public int BonusTransactionStatusId { get; set; }

        public PBonusTransactionStatus BonusTransactionStatus { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }

        public PUser User { get; set; }

        public bool IsDeleted { get; set; }
    }
}
