using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionIdentityServer.Model
{
    public class ExternalUser
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ExternalProvider { get; set; }

        public string ProviderUserId { get; set; }

        public string Email { get; set; }

        [Required]
        public string Provider { get; set; }
    }
}
