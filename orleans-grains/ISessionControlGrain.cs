using System;
using System.Threading.Tasks;
using Orleans;

namespace orleans_grains
{
    public interface ISessionControlGrain: IGrainWithStringKey
    {
        Task Login(string userId);
        Task Logout(string userId);
        Task<int> GetActiveUserCount();
    }
}