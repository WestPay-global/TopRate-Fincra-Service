using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fincra.Interfaces
{
    public interface IHttpDataClient
    {
        Task<T> MakeRequest<T>(string url);
        Task<T> MakeRequest<T>(object data, string url, Dictionary<string, string> headers);
        Task<T> MakeRequest<T>(string url, Dictionary<string, string> headers);
    }
}