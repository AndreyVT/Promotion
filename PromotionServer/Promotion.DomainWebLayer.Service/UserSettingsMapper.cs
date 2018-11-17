namespace Promotion.DomainWebLayer.Mappers
{
    using System;
    using System.Collections.Generic;

    using Promotion.Domain.Entities;
    using Promotion.Domain.Services;
    using Promotion.Server.Web.ViewModel;
    
    public class UserSettingsMapper
    {
        private readonly UserRolesService _userRolesService;
        private readonly UserPermissionsService _userPermissionsService;

        public UserSettingsMapper(UserRolesService userRolesService, UserPermissionsService userPermissionsService)
        {
            this._userRolesService = userRolesService;
            this._userPermissionsService = userPermissionsService;
        }

        private UserSettingsVM MapToView(List<PUserRole> userRoles, List<Tuple<string, int>> userSegmentsPermissions)
        {
            UserSettingsVM userSettingsVM = new UserSettingsVM();

            foreach(var role in userRoles)
            {
                userSettingsVM.Roles.Add(new RoleVM { Id = role.Id.ToString(), Name = role.Role.Name });
            }

            userSettingsVM.Segments = userSegmentsPermissions;

            return userSettingsVM;
        }

        public UserSettingsVM GetUserSettings(string userLogin)
        {
            List<PUserRole> userRoles = _userRolesService.GetRolesByUserLogin(userLogin);
            List<Tuple<string, int>> userSegmentsPermissions = _userPermissionsService.GetSegmentsPermissions(userLogin, userRoles);

            return MapToView(userRoles, userSegmentsPermissions);
        }
    }
}
