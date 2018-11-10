using System.Threading.Tasks;
using Promotion.Common.DomainEntities;

namespace Promotion.Common.Interfaces
{
    public interface IUserSynchronizer
    {
        void SyncUsers();
        Task<IdentityServerUserInfo> GetUserInfo(string userLogin);
    }
}
