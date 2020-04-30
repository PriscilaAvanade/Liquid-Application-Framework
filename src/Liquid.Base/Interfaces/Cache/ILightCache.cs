using System;
using System.Threading.Tasks;
using Liquid.Base.Interfaces;

namespace Liquid.Interfaces
{
    /// <summary>
    /// Cache interface for Microservice
    /// </summary>
    [Obsolete("Prefer to use the standard IDistributedCache over ILightCache. \n This interface will be removed in later version. Please refrain from accessing it.")]
    public interface ILightCache : IWorkbenchService
    {
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);

        void Set<T>(string key, T value);
        Task SetAsync<T>(string key, T value);

        void Refresh(string key);
        Task RefreshAsync(string key);

        void Remove(string key);
        Task RemoveAsync(string key);
    }
}
