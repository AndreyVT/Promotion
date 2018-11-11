namespace Promotion.DomainWebLayer.Mappers
{
    using System.Collections.Generic;

    using Promotion.Domain.Entities;
    using Promotion.Domain.Services;
    using Promotion.Server.Web.ViewModel;
    
    public class UserSettingsMapper
    {
        private readonly UserRolesService _userRolesService;

        public UserSettingsMapper(UserRolesService userRolesService)
        {
            this._userRolesService = userRolesService;
        }

        private UserSettingsVM MapToView(List<PUserRole> userRoles)
        {
            UserSettingsVM userSettingsVM = new UserSettingsVM();

            foreach(var role in userRoles)
            {
                userSettingsVM.Roles.Add(new RoleVM { Id = role.Id.ToString(), Name = role.Role.Name });
            }

            return userSettingsVM;
        }

        public UserSettingsVM GetUserSettings(string userLogin)
        {
            List<PUserRole> userRoles = _userRolesService.GetRolesByUserLogin(userLogin);

            return MapToView(userRoles);
        }
    }
}
