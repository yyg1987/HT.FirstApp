using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IBlogsService
    {
        Task<ResponseMessage> GetArticlesAsync(string blogApp, int pageIndex=1, int pageSize = 20);
    }
}
