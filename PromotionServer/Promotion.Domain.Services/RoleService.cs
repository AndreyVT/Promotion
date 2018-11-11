namespace Promotion.Domain.Services
{
    using System;
    using System.Linq;
    using Promotion.Core.Services;
    using Promotion.DataBase.Base;
    using Promotion.Domain.Entities;

    public class RoleService: BaseService
    {
        private IBaseRepository<PRole, int> _roleRepository;

        public RoleService(IBaseRepository<PRole, int> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public PRole GetRoleByLogicalName(string roleLogicalName)
        {
            return _roleRepository.Get().Where(c => c.LogicalName == roleLogicalName).FirstOrDefault();
        }
    }
}
