namespace Promotion.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Promotion.Core.Services;
    using Promotion.DataBase.Base;
    using Promotion.Domain.Entities;

    public class UserRolesService : BaseService
    {
        private IBaseRepository<PUserRole, int> _userRoleRepository;

        public UserRolesService(IBaseRepository<PUserRole, int> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public List<PUserRole> GetRolesByUserLogin(string userLogin)
        {
            var result = _userRoleRepository.Get().Where(c => c.User.ExternalId == userLogin).ToList();
            return result;
        }

        public PRole GetRoleByLogicalName(string roleLogicalName)
        {
            var userRole = _userRoleRepository.Get().Where(c => c.Role.LogicalName == roleLogicalName).FirstOrDefault();

            return userRole != null ? userRole.Role : null;
        }

        public void Add(PUserRole pUserRole)
        {
            _userRoleRepository.Add(pUserRole);
        }
    }
}
