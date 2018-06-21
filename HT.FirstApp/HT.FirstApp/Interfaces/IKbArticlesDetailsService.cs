using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IKbArticlesDetailsService
    {
        Task<ResponseMessage> GetKbArticlesAsync(int id);
    }
}
