using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IKbArticlesService
    {
        Task<ResponseMessage> GetKbArticlesAsync(int pageIndex = 1,int pageSize = 20);
    }
}
