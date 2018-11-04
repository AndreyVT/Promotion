using System.ComponentModel.DataAnnotations;

namespace PromotionIdentityServer.ViewModels
{
    public class RegisterUserModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RepeatPassword { get; set; }
    }
}
