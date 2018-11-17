namespace Promotion.Server.Web.ViewModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Настройки пользователя
    /// </summary>
    public class UserSettingsVM
    {
        public UserSettingsVM()
        {
            Roles = new List<RoleVM>();
            Segments = new List<Tuple<string, int>>();
        }

        public List<RoleVM> Roles { get; set; }

        public RoleVM Role {
            get
            {
                return Roles.Count > 0 ? Roles[0] : null;
            }
        }

        public List<Tuple<string, int>> Segments { get; set; }
    }
}
