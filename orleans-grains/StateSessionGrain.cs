using System;
using System.Threading.Tasks;
using Orleans;

namespace orleans_grains
{
    public class StateSessionGrain: Grain<LoginState>, ISessionControlGrain
    {
        public Task Login(string userId)
        {
            var appName = this.GetPrimaryKeyString();
            this.State.LoginUsers.Add(userId);
            this.WriteStateAsync();

            Console.WriteLine($"State current active users count of {appName} is {this.State.Count}");
            return Task.CompletedTask;
        }

        public Task Logout(string userId)
        {
            //获取当前Grain的身份标识
            var appName = this.GetPrimaryKey();
            this.State.LoginUsers.Remove(userId);
            this.WriteStateAsync();

            Console.WriteLine($"State current active users count of {appName} is {this.State.Count}");
            return Task.CompletedTask;
        }

        public Task<int> GetActiveUserCount()
        {
            return Task.FromResult(this.State.Count);
        }
    }
}