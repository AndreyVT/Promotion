namespace Promotion.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Promotion.Common.Dictionaries;
    using Promotion.Core.Services;
    using Promotion.Domain.Entities;
    using Promotion.Domain.Entities.Enums;

    /// <summary>
    /// Пока заглушка
    /// </summary>
    public class UserPermissionsService : BaseService
    {
        private Dictionary<string, List<Tuple<string, int>>> permissionsSegments = new Dictionary<string, List<Tuple<string, int>>>();
        private List<string> segments = new List<string> { "promote", "users", "management" };

        public UserPermissionsService()
        {
            permissionsSegments[Roles.Admin.LogicalName] = new List<Tuple<string, int>>();
            permissionsSegments[Roles.Manager.LogicalName] = new List<Tuple<string, int>>();
            foreach (var segment in segments)
            {
                permissionsSegments[Roles.Admin.LogicalName].Add(new Tuple<string, int>(segment, (int)PermissionsEnum.Write));
                permissionsSegments[Roles.Manager.LogicalName].Add(new Tuple<string, int>(segment, (int)PermissionsEnum.Write));
            }

            permissionsSegments[Roles.User.LogicalName] = new List<Tuple<string, int>>();
            permissionsSegments[Roles.User.LogicalName].Add(new Tuple<string, int>("promote", (int)PermissionsEnum.Write));
        }

        /// <summary>
        /// Пока заглушка
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public List<Tuple<string, int>> GetSegmentsPermissions(string userLogin, List<PUserRole> userRoles)
        {
            if (userRoles.Any(c => c.Role.LogicalName == Roles.Admin.LogicalName))
            {
                return permissionsSegments[Roles.Admin.LogicalName];
            }

            if (userRoles.Any(c => c.Role.LogicalName == Roles.Manager.LogicalName))
            {
                return permissionsSegments[Roles.Manager.LogicalName];
            }

            if (userRoles.Any(c => c.Role.LogicalName == Roles.User.LogicalName))
            {
                return permissionsSegments[Roles.User.LogicalName];
            }

            throw new Exception("UserPermissionsService :: не поддерживаемая роль.");
        }
    }
}
