namespace Promotion.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRoles")]
    public class PUserRole: BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public PUser User { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public PRole Role { get; set; }
    }
}
