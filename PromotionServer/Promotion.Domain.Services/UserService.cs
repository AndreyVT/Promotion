namespace Promotion.Domain.Services
{
    using Promotion.Core.Services;
    using Promotion.DataBase.Base;
    using Promotion.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : BaseService
    {
        private IBaseRepository<PUser, int> _userRepository;

        public UserService(IBaseRepository<PUser, int> userRepository)
        {
            _userRepository = userRepository;
        }

        public PUser GetUserByExternalId(string externalId)
        {
            return _userRepository.Get().Where(c => c.ExternalId == externalId).FirstOrDefault();
        }

        public IEnumerable<PUser> Get()
        {
            return _userRepository.Get();
        }

        public async Task<PUser> FindAsync(int id)
        {
            return _userRepository.GetById(id);
        }

        public async Task<PUser> Update(PUser pUser)
        {
            return _userRepository.Update(pUser);
        }

        public async Task<PUser> Create(PUser pUser)
        {
            var result = _userRepository.Add(pUser);

            return result;
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
