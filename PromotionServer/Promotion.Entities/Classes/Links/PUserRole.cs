using Promotion.Entities.Busines;
using Promotion.Entities.Classes.Base;
using Promotion.Entities.Dictionary;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promotion.Entities.Classes.Links
{
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
