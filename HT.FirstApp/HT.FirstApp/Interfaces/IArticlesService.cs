using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IArticlesService
    {
        Task<ResponseMessage> GetArticlesAsync(int position, int pageIndex = 1, int pageSize = 20);
    }
}
